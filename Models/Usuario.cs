namespace productos.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public required string Nombre { get; set; }
        public string Apellido { get; set; }
        public required string CorreoElectronico { get; set; }
        public required string Contrasena { get; set; }
        public required string Rol { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
