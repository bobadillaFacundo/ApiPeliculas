using ApiPeliculas.Data;
using ApiPeliculas.Modelos;
using ApiPeliculas.Repositorio.IRepositorio;

namespace ApiPeliculas.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public CategoriaRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCategoria(Categorias categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            var categoriaDb = _bd.Categoria.Find(categoria.Id);
            if (categoriaDb != null)
            {
                _bd.Entry(categoriaDb).CurrentValues.SetValues(categoria);
            }
            else
            {
                _bd.Update(categoria);
            }
            return Guardar();
        }

        public bool BorrarCategoria(Categorias categoria)
        {
            _bd.Categoria.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categorias categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            _bd.Categoria.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(int id)
        {
            return _bd.Categoria.Any(c => c.Id == id);
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _bd.Categoria.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public Categorias GetCategoria(int CategoriaId)
        {
            return _bd.Categoria.Find(CategoriaId);
        }

        public ICollection<Categorias> GetCategorias()
        {
            return _bd.Categoria.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
