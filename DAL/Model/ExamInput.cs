namespace DAL.Model
{
    public class ExamInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ExamCategoryInput> CategoryList { get; set; }
    }
    public class ExamCategoryInput
    {
        public string CategoryName { get; set; }
        public int NumberOfQuestions { get; set; }
        public int DegreePerQuestion { get; set; }
    }
}
