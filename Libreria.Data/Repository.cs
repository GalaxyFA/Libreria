using Libreria.Data.MainModels;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Data;

public class Repository<T> : IRepository<T> where T : class
{
    public LibreriaContext _libreriaEntities;
    public Repository()
    {
        _libreriaEntities = new LibreriaContext();
    }
    public List<T> GetAll()
    {
        try
        {

            return _libreriaEntities.Set<T>().Select(a => a).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public T? GetById(int id)
    {
        return _libreriaEntities.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        _libreriaEntities.Set<T>().Add(entity);
    }

    public void Del(int id)
    {
        var q = GetById(id);
        _libreriaEntities.Set<T>().Remove(q);
    }

    public void Edit(T entity)
    {
        _libreriaEntities = new LibreriaContext();
        try
        {
            _libreriaEntities.Set<T>().Update(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public int Savechange()
    {
        return _libreriaEntities.SaveChanges();
    }

    public List<T> GetByQuery(IQueryable<T> query)
    {
        return query.ToList();
    }
}
