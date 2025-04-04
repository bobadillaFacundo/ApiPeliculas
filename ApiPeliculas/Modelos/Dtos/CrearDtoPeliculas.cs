using System.ComponentModel.DataAnnotations.Schema;

namespace API_Peliculas.Model.Dtos
{
    public class CrearDtoPeliculas
    {
        public string Nombre { get; set; }

        public int Duracion { get; set; }

        public String RutaImagen { get; set; }

        public string Descripcion { get; set; }

        public enum TipoClasificacion { siete, trece, dieciseis, dieciocho }

        public TipoClasificacion Clasificacion { get; set; }

        public int CategoriaId { get; set; }
        
    }
}
