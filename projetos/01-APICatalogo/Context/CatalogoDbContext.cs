using _01_APICatalogo.Domain;
using Microsoft.EntityFrameworkCore;

namespace _01_APICatalogo.Context;

public class CatalogoDbContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }


    public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options) : base(options)
    {

    }
}
