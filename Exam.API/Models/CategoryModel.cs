namespace Exam.API.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
    public class QuestionModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Body { get; set; }
        public string Answer { get; set; }
        public decimal Degree { get; set; }
    }
}
