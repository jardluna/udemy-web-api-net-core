using _01_APICatalogo.Context;
using _01_APICatalogo.Domain;
using _01_APICatalogo.Interfaces;

namespace _01_APICatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(CatalogoDbContext context) : base(context) { }


    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
