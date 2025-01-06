using BLL.Interface;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repostory
{
    public class GenaricRepostory<T> : IGenaricRepostory<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenaricRepostory(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(T entity) => await _context.Set<T>().AddAsync(entity);

        public async Task Delete(Guid id) => _context.Set<T>().Remove(await GetById(id));

        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();

        public async Task<T> GetById(Guid id) => await _context.Set<T>().FindAsync(id);

        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
