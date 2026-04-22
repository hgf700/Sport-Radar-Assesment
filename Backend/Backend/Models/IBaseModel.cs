namespace Backend.Models;

public class IBaseModel
{

}

public interface IRepositoryStrategy<T> where T : IBaseModel
{
    void Add(T entity);
    T? GetById(int id);
    IEnumerable<T> GetAll();
}