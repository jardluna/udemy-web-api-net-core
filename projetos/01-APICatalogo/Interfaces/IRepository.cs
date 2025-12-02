using _01_APICatalogo.Domain;
using System.Linq.Expressions;

namespace _01_APICatalogo.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T? GetById(Expression<Func<T, bool>> predicate); // Ou T GetById(int id)
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
