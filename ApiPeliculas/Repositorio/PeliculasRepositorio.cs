using System;
using System.Linq;
using API_Peliculas.Model;
using ApiPeliculas.Data;
using ApiPeliculas.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using ApiPeliculas.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API_Peliculas.Repository
{
    public class PeliculasRepositorio : IPeliculasRepositorio
    {
        private readonly ApplicationDbContext _db;


        public PeliculasRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarPeliculas(Peliculas Peliculas)
        {
            Peliculas.FechaCreacion = DateTime.Now;
            var PeliculasDb = _db.Pelicula.Find(Peliculas.ID);
            if (PeliculasDb != null)
            {
                var idCategoria_id = _db.Categoria.Find(Peliculas.CategoriaId);
                if(idCategoria_id != null)
                {
                    _db.Entry(PeliculasDb).CurrentValues.SetValues(Peliculas);
                }else
                {
                    return false;
                }
            }
            else
            {
                _db.Update(Peliculas);
            }
            return Guardar();
        }

        public bool CrearPeliculas(Peliculas Peliculas)
        {
            Peliculas.FechaCreacion = DateTime.Now;
            _db.Pelicula.Add(Peliculas);
            return Guardar();
        }

        public bool EliminarPeliculas(Peliculas Peliculas)
        {
            _db.Pelicula.Remove(Peliculas);
            return Guardar();
        }

        public bool EliminarPeliculas(int id)
        {
            _db.Pelicula.Remove(_db.Pelicula.FirstOrDefault(c => c.ID == id));
            return Guardar();
        }

        public bool ExistePeliculas(string nombre)
        {
            return _db.Pelicula.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }

        public bool ExistePeliculas(int id)
        {
            return _db.Pelicula.Any(c => c.ID == id);
        }

        public Peliculas GetPeliculas(int id)
        {
            return _db.Pelicula.FirstOrDefault(c => c.ID == id);
        }

        public ICollection<Peliculas> GetPeliculass()
        {
            return _db.Pelicula.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }


        ICollection<Peliculas> IPeliculasRepositorio.GetPeliculasPorCategorias(int categoriaID)
        {
             
            if (categoriaID == 0) return _db.Pelicula.ToList();

            IQueryable<Peliculas> peliculas = _db.Pelicula;
                return peliculas.Where(c => c.CategoriaId == categoriaID).ToList();
        }

        IEnumerable<Peliculas> IPeliculasRepositorio.BuscarPelicula(string nombre)
        {
            IQueryable<Peliculas> peliculas = _db.Pelicula;
            if (!string.IsNullOrEmpty(nombre))
            {
                return peliculas.Where(c => c.Nombre.Contains(nombre) || c.Descripcion.Contains(nombre));  
            }

            return peliculas.ToList();

        }

       
    
    
    }




}