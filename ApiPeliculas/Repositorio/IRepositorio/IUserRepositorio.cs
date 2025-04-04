using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;

namespace ApiPeliculas.Repositorio.IRepositorio
{
    public interface IUserRepositorio
    {
        ICollection<Usuario> GetUsers();
        Usuario GetUser(int id);
        bool IsUniqueUser(string username);
        Task<DtoUserLoginRes> Login(DtoLogin dtoLogin);
        Task<DtoUserDate> Registro(DtoUserRegistro dtoUserCreate);
    }
}
