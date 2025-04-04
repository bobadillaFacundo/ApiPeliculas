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
            CreateMap<Categorias, CategoriaDto>().ReverseMap();
            CreateMap<Categorias, CrearCategoriaDto>().ReverseMap();
            CreateMap<Peliculas, DtoPelicula>().ReverseMap(); 
            CreateMap<Peliculas, CrearDtoPeliculas>().ReverseMap();
        }
    }
}
