using System.Collections.ObjectModel;

namespace _01_APICatalogo.Domain;


public class Categoria
{
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }

    public ICollection<Produto>? Produtos { get; set; }

    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
}
