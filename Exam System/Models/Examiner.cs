namespace Exam_System.Models
{
    public class Examiner
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public decimal Degree { get; set; }
    }
}
