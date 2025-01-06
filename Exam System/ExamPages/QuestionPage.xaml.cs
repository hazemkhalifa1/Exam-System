using Exam_System.Models;
using static Exam_System.AccountPages.LoginViewModel;

namespace Exam_System.ExamPages;

public partial class QuestionPage : ContentPage
{
    public decimal TotalDegree = 0;
    private Exam _exam;
    private ApiService _api = new ApiService();
    public QuestionPage(Exam exam)
    {
        _exam = exam;
        InitializeComponent();
        for (int i = 0; i < _exam.Questions.Count; i++)
        {
            if (i == (_exam.Questions.Count - 1))
            {
                BindingContext = new QuestionViewModel(_exam.Questions[i], ref TotalDegree, "انهاء");
                _api.PostAsync<ApiResponse>("Exam/Examenr", new { ExamId = _exam.Id, UserId = UserId, Degree = TotalDegree });
            }
            else
                BindingContext = new QuestionViewModel(_exam.Questions[i], ref TotalDegree, "التالي");

        }
    }
}