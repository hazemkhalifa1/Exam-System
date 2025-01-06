using Exam_System.ExamPages;

namespace Exam_System;

public partial class CreateExam : ContentPage
{
    public CreateExam()
    {
        InitializeComponent();
        BindingContext = new CreateExamViewModel();
    }
}