namespace Exam_System.AccountPages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = new RegisterViewModel();

    }
}