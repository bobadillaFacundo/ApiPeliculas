namespace ApiPeliculas.Modelos.Dtos
{
    public class DtoUserLoginRes
    {
        public Usuario User { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
