using System.Text.Json.Serialization;

namespace DAL.Entities
{
    public class Question : BaseEntity
    {
        public string Body { get; set; }
        public string Answer { get; set; }
        public decimal Degree { get; set; }
        [JsonIgnore]
        public Category? category { get; set; }

    }
}
