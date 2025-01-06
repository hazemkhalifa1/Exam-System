using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserExam> UserExams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserExam>()
                .HasKey(o => new { o.UserId, o.ExamId });
            builder.Entity<IdentityUserLogin<string>>().HasKey(iul => new { iul.LoginProvider, iul.ProviderKey });
            builder.Entity<IdentityUserRole<string>>().HasKey(iur => new { iur.UserId, iur.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(iut => new { iut.UserId, iut.LoginProvider, iut.Name });
        }

    }
}
