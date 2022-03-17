namespace InternetShopBackend.Models;

public interface IRepository<TEntity> where TEntity : class
{
    List<TEntity> Get();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}