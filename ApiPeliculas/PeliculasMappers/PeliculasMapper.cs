using API_Peliculas.Model;
using API_Peliculas.Model.Dtos;
using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using AutoMapper;

namespace ApiPeliculas.PeliculasMapper
{
    public class PeliculasMapper : Profile
    {
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();
            CreateMap<Peliculas, DtoPelicula>().ReverseMap(); // <--- Aqui>
            CreateMap<Peliculas, CrearDtoPeliculas>().ReverseMap();
        }
    }
}
