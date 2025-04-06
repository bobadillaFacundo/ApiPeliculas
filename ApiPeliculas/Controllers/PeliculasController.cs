using System.Security.Permissions;
using API_Peliculas.Model;
using API_Peliculas.Model.Dtos;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Modelos;
using ApiPeliculas.Repositorio;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculasRepositorio _peliculasRepositorio;
        private readonly IMapper _mapper;
        public PeliculasController(IPeliculasRepositorio categoriaRepositorio, IMapper mapper)
        {
            _peliculasRepositorio = categoriaRepositorio;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        // GET: api/<PeliculasController>
        [HttpGet(Name = "GetPeliculas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetPeliculas()
        {
            var listaPeliculas = _peliculasRepositorio.GetPeliculass();
            var listaPeliculasDTO = new List<DtoPelicula>();
            foreach (var pelicula in listaPeliculas)
            {
                listaPeliculasDTO.Add(_mapper.Map<DtoPelicula>(pelicula));
            }

            return Ok(listaPeliculasDTO);
        }

        // GET api/<PeliculasController>/5
        [HttpGet("{id}", Name = "GetPeliculaId")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPeliculaId(int id)
        {

            var itemPelicula = _peliculasRepositorio.GetPeliculas(id);
            if (itemPelicula == null)
            {
                return NotFound();
            }
            var itemPeliculaDTO = _mapper.Map<Peliculas>(itemPelicula);
            return Ok(itemPeliculaDTO);
        }



        [HttpPatch("{id:int}", Name = "ActualizarPelicula")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPelicula(int id, [FromBody] CrearDtoPeliculas peliculaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (peliculaDto == null)
            {
                return BadRequest(ModelState);
            }

            var peliculaExiste = _peliculasRepositorio.GetPeliculas(id);
            if (peliculaExiste == null)
            {
                return NotFound($"La pelicula no existe {peliculaExiste}");
            }

            var pelicula = _mapper.Map<Peliculas>(peliculaDto);
            if (!_peliculasRepositorio.ActualizarPeliculas(pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando la pelicula {pelicula.Descripcion}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPost(Name = "CrearPelicula")]
        [ProducesResponseType(201, Type = typeof(CrearDtoPeliculas))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearPelicula([FromBody] CrearDtoPeliculas crearpeliculadto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (crearpeliculadto is null)
                {
                    return BadRequest(ModelState);
                }

                if (_peliculasRepositorio.ExistePeliculas(crearpeliculadto.Nombre))
                {
                    ModelState.AddModelError("", "La pelicula ya existe");
                    return StatusCode(StatusCodes.Status404NotFound, ModelState);
                }

                var peliculas = _mapper.Map<Peliculas>(crearpeliculadto);

                if (!_peliculasRepositorio.CrearPeliculas(peliculas))
                {
                    ModelState.AddModelError("", $"Algo salio mal guardando la pelicula {peliculas.Nombre}");
                    return StatusCode(404, ModelState);
                }
                return CreatedAtRoute("GetPeliculas", new { peliculaId = peliculas.ID }, peliculas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // DELETE api/<PeliculasController>/5
        [HttpDelete("{id}", Name = "EliminarPelicula")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EliminarPelicula(int id)
        {
            if (!_peliculasRepositorio.ExistePeliculas(id))
            {
                return NotFound($"La pelicula con id {id} no existe");
            }
            var pelicula = _peliculasRepositorio.GetPeliculas(id);
            if (!_peliculasRepositorio.EliminarPeliculas(pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando la pelicula {pelicula.Descripcion}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpGet("GetPeliculasCategoria/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPeliculasCategoria(int id)
        {
            var itemPelicula = _peliculasRepositorio.GetPeliculasPorCategorias(id);
                if (itemPelicula.Count == 0)
            {
                return NotFound();
            }
            var itemPeliculaDTO = new List<DtoPelicula>();
            foreach (var pelicula in itemPelicula)
            {
                itemPeliculaDTO.Add(_mapper.Map<DtoPelicula>(pelicula));
            }
            return Ok(itemPeliculaDTO);
        }

        [HttpGet("Buscar")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Buscar(string nombre)
        {
            try
            {
                var peliculas = _peliculasRepositorio.BuscarPelicula(nombre);
                if (peliculas.Any())
                {
                    return Ok(peliculas);
                }
               return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}


