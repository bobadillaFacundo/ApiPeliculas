using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos
{
    public class Categoria
    {

        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        [Required]       
        public DateTime FechaCreacion { get; set; }      
        
        internal string Descripcion;

    }
}
