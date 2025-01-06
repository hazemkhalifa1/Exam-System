namespace DAL.Entities
{
    public class Exam : BaseEntity
    {
        public string Name { get; set; }
        public decimal Total_Degree { get; set; }
        public List<Question> Questions { get; set; }
        public int Num_Of_Examiners { get; set; }
        public List<UserExam> Users { get; set; }
    }
}
