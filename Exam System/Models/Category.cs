namespace Exam_System.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<Question> Questions { get; set; }
        public int Num_Of_Q { get; set; }
    }
}
