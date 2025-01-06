namespace Exam_System.AccountPages;

public partial class UsersPage : ContentPage
{
    public UsersPage()
    {
        InitializeComponent();
        BindingContext = new UserListViewModel();
    }
}