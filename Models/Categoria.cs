namespace productos.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public required string Nombre { get; set;}
        public string Descripcion { get; set;}
        public DateTime FechaCreacion { get; set;}

    }
}
