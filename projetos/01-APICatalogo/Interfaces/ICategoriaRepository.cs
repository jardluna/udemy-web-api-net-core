using _01_APICatalogo.Domain;

namespace _01_APICatalogo.Interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    IEnumerable<Categoria> GetProdutosAll();
}
