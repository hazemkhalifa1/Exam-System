namespace Exam_System.Models
{
    public class Exam
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Num_Of_Examiners { get; set; }
        public decimal Total_Degree { get; set; }
        public List<Question> Questions { get; set; }
    }
}
