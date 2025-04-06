using System.Net;
using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserRepositorio _userRepositorio;
        private readonly IMapper _mapper;
        protected RespuestaApi _respuestaApi;
        public UsuariosController(IUserRepositorio userRepositorio, IMapper mapper)
        {
            _userRepositorio = userRepositorio;
            _mapper = mapper;
            this._respuestaApi = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _userRepositorio.GetUsers();
            var listaUsuariosDTO = new List<DtoUser>();
            foreach (var usuario in listaUsuarios)
            {
                listaUsuariosDTO.Add(_mapper.Map<DtoUser>(usuario));
            }
            return Ok(listaUsuariosDTO);
        }



        [HttpGet("{id:int}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuario(int id)
        {
            var itemUsuario = _userRepositorio.GetUser(id);
            if (itemUsuario == null)
            {
                return NotFound();
            }
            var itemUsuarioDTO = _mapper.Map<DtoUser>(itemUsuario);
            return Ok(itemUsuarioDTO);
        }

        [HttpPost("CrearUsuario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CrearUsuario([FromBody] DtoUserRegistro usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var booleano = _userRepositorio.IsUniqueUser(usuarioDto.User);
            if (!booleano)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("El usuario ya existe");
                return BadRequest(_respuestaApi);
            }

            var usuario = _userRepositorio.Registro(usuarioDto);
            if (usuario == null)
            {         
            _respuestaApi.StatusCode = HttpStatusCode.Created;
            _respuestaApi.IsSuccess = true;
            _respuestaApi.ErrorMessages.Add("El usuario se creo correctamente");
            return BadRequest(_respuestaApi);
             }

            return NotFound();
        }

     

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] DtoLogin usuarioDto)
        {
            
            var resultLogin = await _userRepositorio.Login(usuarioDto);
            
            if (resultLogin.User == null || string.IsNullOrEmpty(resultLogin.Token))
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("El usuario o password no existe");
                return BadRequest(_respuestaApi);
            }

                _respuestaApi.StatusCode = HttpStatusCode.Created;
                _respuestaApi.IsSuccess = true;
                _respuestaApi.Result = resultLogin;
                return Ok(_respuestaApi);
         
        }
    }
}
