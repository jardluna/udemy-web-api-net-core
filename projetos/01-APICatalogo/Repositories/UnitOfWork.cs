using _01_APICatalogo.Context;
using _01_APICatalogo.Interfaces;

namespace _01_APICatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;
        private readonly CatalogoDbContext _context;


        public UnitOfWork(CatalogoDbContext context)
        {
            _context = context;
        }


        public IProdutoRepository ProdutoRepository
        {
            get { return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context); }
        }


        public ICategoriaRepository CategoriaRepository
        {
            get { return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context); }
        }


        public void Commit()
        {
            _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
