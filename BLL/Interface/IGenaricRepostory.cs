using DAL.Entities;

namespace BLL.Interface
{
    public interface IGenaricRepostory<T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(Guid id);
        public void Update(T entity);
        public Task Delete(Guid id);
        public Task Create(T entity);
    }
}
