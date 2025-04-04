using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos.Dtos
{
    public class DtoUserRegistro
    {
 
        [Required(ErrorMessage = "El user es requerido")]
        public string User { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage =  "El password es requerido")]
        public string Password { get; set; }
        public string Role { get; internal set; }
    }
}
