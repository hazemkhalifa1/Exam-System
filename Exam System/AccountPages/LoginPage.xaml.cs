namespace Exam_System.AccountPages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }
}