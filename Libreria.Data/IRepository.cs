using Libreria.Data.MainModels;

namespace Libreria.Data;

public interface IRepository<T>
{
    public List<T> GetAll();
    public T? GetById(int id);
    public void Add(T entity);
    public void Edit(T entity);
    public void Del(int id);
    public List<T> GetByQuery(IQueryable<T> query);
    public int Savechange();
}
