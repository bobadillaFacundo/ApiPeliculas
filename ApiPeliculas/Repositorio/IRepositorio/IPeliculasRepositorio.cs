using API_Peliculas.Model;

namespace ApiPeliculas.Repositorio.IRepositorio
{
    public interface IPeliculasRepositorio


    {
        ICollection<Peliculas> GetPeliculass();
        ICollection<Peliculas> GetPeliculasPorCategorias(int categoriaID);
        IEnumerable<Peliculas> BuscarPelicula(string nombre);
        Peliculas GetPeliculas(int id);
        bool CrearPeliculas(Peliculas peliculas);
        bool ActualizarPeliculas(Peliculas peliculas);
        bool EliminarPeliculas(Peliculas peliculas);
        bool ExistePeliculas(string nombre);
        bool ExistePeliculas(int id);
        bool EliminarPeliculas(int id);
        bool Guardar(); 
    }
}
