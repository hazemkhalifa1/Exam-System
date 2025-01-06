using BLL.Interface;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repostory
{
    public class ExamRepostory : GenaricRepostory<Exam>, IExamRepostory
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public ExamRepostory(AppDbContext context, UserManager<AppUser> userManager) : base(context)
        {
            _userManager = userManager;
            _context = context;
        }

        public void Update(UserExam userExam)
        {
            Exam exam = userExam.exam;
            exam.Users.Add(userExam);
            _context.Update(exam);
        }
        public List<UserExam> GetExamenr(Guid id)
            => _context.Exams.Where(e => e.ID == id).Include(e => e.Users).FirstOrDefault().Users.ToList();

        public void AddUserExam(UserExam userExam) => _context.UserExams.Add(userExam);

        public List<Question> GetQuestions(Guid id)
            => _context.Exams.Where(e => e.ID == id).Include(e => e.Questions).FirstOrDefault().Questions.ToList();
    }
}
