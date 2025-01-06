namespace Exam.API.Models
{
    public class ExamenrVM
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public decimal Degree { get; set; }
    }
}
