using _01_APICatalogo.Domain;

namespace _01_APICatalogo.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
