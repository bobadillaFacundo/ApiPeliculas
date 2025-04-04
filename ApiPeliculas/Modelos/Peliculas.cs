using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiPeliculas.Modelos;

namespace API_Peliculas.Model
{
    public class Peliculas
    {
        [Key]
        public int ID { get; set; }

        public string Nombre { get; set; }

        public int Duracion { get; set; }

        public String RutaImagen { get; set; } 

        public string Descripcion { get; set; }

        public enum TipoClasificacion {siete,trece,dieciseis,dieciocho}

        public TipoClasificacion Clasificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]

        public Categoria Categoria { get; set; }


    }
}
