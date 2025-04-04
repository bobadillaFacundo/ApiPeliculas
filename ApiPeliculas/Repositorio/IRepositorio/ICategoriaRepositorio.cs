using ApiPeliculas.Modelos;

namespace ApiPeliculas.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio
    {
        ICollection<Categorias> GetCategorias();
        Categorias GetCategoria(int CategoriaId);
        bool ExisteCategoria(int id);
        bool ExisteCategoria(string nombre);

        bool CrearCategoria(Categorias categoria);
        bool ActualizarCategoria(Categorias categoria);
        bool BorrarCategoria(Categorias categoria);
        bool Guardar();
    }
}
