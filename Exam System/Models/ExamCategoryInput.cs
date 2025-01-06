namespace Exam_System.Models
{
    public class ExamCategoryInput
    {
        public string CategoryName { get; set; }
        public int NumberOfQuestions { get; set; }
        public decimal DegreePerQuestion { get; set; }
        public List<Question> Questions { get; set; }
    }

}
