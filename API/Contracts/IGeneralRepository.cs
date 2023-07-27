using API.Models;

namespace API.Contracts;

public interface IGeneralRepository<TEntity>
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetByGuid(Guid id);
    TEntity? Create(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
    void Clear();
}
