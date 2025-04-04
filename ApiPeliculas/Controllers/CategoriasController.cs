using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_Peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IMapper _mapper;
        public CategoriasController(ICategoriaRepositorio categoriaRepositorio, IMapper mapper)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _categoriaRepositorio.GetCategorias();
            var listaCategoriasDTO = new List<CategoriaDto>();
            foreach (var categoria in listaCategorias)
            {
                listaCategoriasDTO.Add(_mapper.Map<CategoriaDto>(categoria));
            }
            return Ok(listaCategoriasDTO);
        }

        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = _categoriaRepositorio.GetCategoria(categoriaId);
            if (itemCategoria == null)
            {
                return NotFound();
            }
            var itemCategoriaDTO = _mapper.Map<CategoriaDto>(itemCategoria);
            return Ok(itemCategoriaDTO);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearCategoria([FromBody] CrearCategoriaDto crearCatgeoriaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (crearCatgeoriaDto is null)
                {
                    return BadRequest(ModelState);
                }

                if (_categoriaRepositorio.ExisteCategoria(crearCatgeoriaDto.Nombre))
                {
                    ModelState.AddModelError("", "La categoria ya existe");
                    return StatusCode(StatusCodes.Status404NotFound, ModelState);
                }

                var categoria = _mapper.Map<Categorias>(crearCatgeoriaDto);

                if (!_categoriaRepositorio.CrearCategoria(categoria))
                {
                    ModelState.AddModelError("", $"Algo salio mal guardando la categoria {categoria.Nombre}");
                    return StatusCode(404, ModelState);
                }
                return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    
     [HttpPatch("{categoriaId:int}", Name = "CambiarNombrePatch")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CambiarNombrePatch(int categoriaId, [FromBody] CategoriaDto CatgeoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (CatgeoriaDto == null)
            {
                return BadRequest(ModelState);
            }
            var categoria = _categoriaRepositorio.GetCategoria(categoriaId);
            if (categoria == null)
            {
                return NotFound($"La categoria con id {categoriaId} no existe");
            }
            var itemCategoria = _mapper.Map<Categorias>(CatgeoriaDto);

            if (!_categoriaRepositorio.ActualizarCategoria(itemCategoria))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando la categoria {itemCategoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpPut("{categoriaId:int}", Name = "CambiarNombrePut")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CambiarNombrePut(int categoriaId, [FromBody] CategoriaDto CatgeoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if ( CatgeoriaDto == null)
            {
                return BadRequest(ModelState);
            }
            var categoria = _categoriaRepositorio.GetCategoria(categoriaId);
            if (categoria == null)
            {
                return NotFound($"La categoria con id {categoriaId} no existe");
            }


            var itemCategoria = _mapper.Map<Categorias>(CatgeoriaDto);

            if (!_categoriaRepositorio.ActualizarCategoria(itemCategoria))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando la categoria {itemCategoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{categoriaId:int}", Name = "EliminarCategoria")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EliminarCategoria(int categoriaId)
        {
            if (!_categoriaRepositorio.ExisteCategoria(categoriaId))
            {
                return NotFound($"La categoria con id {categoriaId} no existe");
            }
            var categoria = _categoriaRepositorio.GetCategoria(categoriaId);
            if(!_categoriaRepositorio.BorrarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando la categoria {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

