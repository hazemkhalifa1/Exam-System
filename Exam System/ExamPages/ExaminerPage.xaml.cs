namespace Exam_System.ExamPages;

public partial class ExaminerPage : ContentPage
{
    public ExaminerPage(Guid examId)
    {
        InitializeComponent();
        BindingContext = new ExaminersViewModel(examId);
    }
}