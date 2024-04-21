namespace productos.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public required string Nombre { get; set; }

        public string Descripcion { get; set; }

        public required int Precio { get; set; }

        public int Cantidad { get; set; }

        public string Imagen { get; set; }

        public DateTime FechaCreacion { get; set; }

        public required int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }


    }
}
