using _01_APICatalogo.Context;
using _01_APICatalogo.Domain;
using _01_APICatalogo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _01_APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(CatalogoDbContext context) : base(context) { }


    public IEnumerable<Categoria> GetProdutosAll()
    {
        return _context.Categorias.Include(p => p.Produtos).ToList();
    }
}
