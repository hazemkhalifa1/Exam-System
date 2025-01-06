namespace BLL.Interface
{
    public interface IUnitOfWork
    {
        public ICategoryRepostory categoryRepostory { get; set; }
        public IExamRepostory ExamRepostory { get; set; }
        public int SaveChanges();
    }
}
