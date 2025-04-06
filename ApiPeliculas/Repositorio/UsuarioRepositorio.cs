using System.Text;
using ApiPeliculas.Data;
using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Repositorio.IRepositorio;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ApiPeliculas.Repositorio
{
    public class UsuarioRepositorio : IUserRepositorio
    {
        private readonly ApplicationDbContext _db;
        private string clave;

        public UsuarioRepositorio(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            clave = configuration.GetValue<string>("ApiSettings:key");
        }

        public Usuario GetUser(int id)
        {
            return _db.Usuario.Where(c => c.Id == id).FirstOrDefault();
        }


        public bool IsUniqueUser(string username)
        {
            var result = _db.Usuario.Where(x => x.User.ToLower() == username.ToLower()).FirstOrDefault();
            if (result == null)
            {
                return true;
            }
            return false;
        }

        public async Task<DtoUserLoginRes> Login(DtoLogin dtoLogin)
        {
            var passwordEncriptado = obtenermd5(dtoLogin.Password);
            var user = _db.Usuario.Where(x => x.User.ToLower() == dtoLogin.User.ToLower() && x.Password == passwordEncriptado).FirstOrDefault();
            if (user == null)
            {
                return new DtoUserLoginRes() { User = null, Token = "" };
            }
            var manejotoken = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(clave);
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.User.ToString()),
                    new Claim(ClaimTypes.Role, user.Rol)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = manejotoken.CreateToken(tokenDescriptor);
            return new DtoUserLoginRes() { Token = manejotoken.WriteToken(token), User = user };
        }

        public async Task<DtoUserDate> Registro(DtoUserRegistro dtoUserCreate)
        {
            var passwordEncriptado = obtenermd5(dtoUserCreate.Password);

            Usuario usuario = new Usuario()
            {
                User = dtoUserCreate.User,
                Password = passwordEncriptado,
                Nombre = dtoUserCreate.Nombre,
                Rol = dtoUserCreate.Role
            };
            _db.Add(usuario);
            await _db.SaveChangesAsync();
            usuario.Password = passwordEncriptado;
            return new DtoUserDate() { Username = usuario.User, Password = passwordEncriptado };
        }

        Usuario IUserRepositorio.GetUser(int id)
        {
            return _db.Usuario.Where(c => c.Id == id).FirstOrDefault() ;
        }

        ICollection<Usuario> IUserRepositorio.GetUsers()
        {
            return _db.Usuario.ToList();
        }

        private string obtenermd5(string password)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


    }
}

