namespace DAL.Entities
{
    public class UserExam
    {
        public Guid ExamId { get; set; }
        public Exam exam { get; set; }
        public Guid UserId { get; set; }
        public AppUser user { get; set; }
        public decimal ExamResult { get; set; }
    }
}
