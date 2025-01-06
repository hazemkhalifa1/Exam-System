namespace Exam_System.Models
{
    public class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Body { get; set; }
        public string Answer { get; set; }
        public decimal Degree { get; set; }
    }
}
