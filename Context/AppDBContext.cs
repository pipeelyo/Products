using Microsoft.EntityFrameworkCore;
using productos.Models;

namespace productos.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
            
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Usuario> Usuarios { get; set; } 


    }
}
