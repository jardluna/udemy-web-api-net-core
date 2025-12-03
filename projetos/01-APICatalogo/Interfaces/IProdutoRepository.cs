using _01_APICatalogo.Domain;

namespace _01_APICatalogo.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    public IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
