using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public Guid Id { get; set; } = new Guid();
        public List<UserExam> userExams { get; set; }
    }
}
