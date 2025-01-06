namespace DAL.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }
    }
}
