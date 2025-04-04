using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos.Dtos
{
    public class DtoLogin
    {
     [Required(ErrorMessage = "El campo es requerido")]
        public string User { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string Password { get; set; }
     
    }
}
