using BLL.Interface;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Repostory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            categoryRepostory = new CategoryRepostory(context);
            ExamRepostory = new ExamRepostory(context, userManager);
        }

        public ICategoryRepostory categoryRepostory { get; set; }
        public IExamRepostory ExamRepostory { get; set; }

        public int SaveChanges() => _context.SaveChanges();
    }
}
