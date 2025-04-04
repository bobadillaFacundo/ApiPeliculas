using API_Peliculas.Model;
using ApiPeliculas.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {            
        }

        //Aquí pasar todas las entidades (Modelos)
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Peliculas> Pelicula { get; set; }
    }
}
