using _01_APICatalogo.Domain;

namespace _01_APICatalogo.Interfaces
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        void Commit();
    }
}
