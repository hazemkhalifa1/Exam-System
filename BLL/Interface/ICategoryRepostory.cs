using DAL.Entities;

namespace BLL.Interface
{
    public interface ICategoryRepostory : IGenaricRepostory<Category>
    {
        public Task<Category> GetByName(string name);
        public IEnumerable<string> GetAllNames();
        public List<Question> GetQuestions(Guid id);
    }
}
