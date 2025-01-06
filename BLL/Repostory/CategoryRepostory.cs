using BLL.Interface;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repostory
{
    public class CategoryRepostory : GenaricRepostory<Category>, ICategoryRepostory
    {
        private readonly AppDbContext _context;
        public CategoryRepostory(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll() => await _context.Categories.Include(c => c.Questions).ToListAsync();
        public IEnumerable<string> GetAllNames() => _context.Categories.Select(c => c.Name);
        public async Task<Category> GetByName(string name) => await _context.Categories.Where(c => c.Name == name).Include(c => c.Questions).FirstOrDefaultAsync();
        public List<Question> GetQuestions(Guid id)
            => _context.Categories.Where(c => c.ID == id).Include(c => c.Questions).FirstOrDefault().Questions.ToList();
    }
}
