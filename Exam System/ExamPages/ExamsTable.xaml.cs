using Exam_System.ExamPages;

namespace Exam_System;

public partial class ExamsTable : ContentPage
{
    public ExamsTable()
    {
        InitializeComponent();
        BindingContext = new ExamsTableViewModel();
    }
}