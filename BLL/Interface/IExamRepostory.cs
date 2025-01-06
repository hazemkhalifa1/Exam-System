using DAL.Entities;

namespace BLL.Interface
{
    public interface IExamRepostory : IGenaricRepostory<Exam>
    {
        public void Update(UserExam userExam);
        public void AddUserExam(UserExam userExam);
        public List<UserExam> GetExamenr(Guid id);
        public List<Question> GetQuestions(Guid id);
    }
}
