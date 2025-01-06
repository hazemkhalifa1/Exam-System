namespace Exam_System.AccountPages;

public partial class EditUserPage : ContentPage
{
    public EditUserPage(UserDto selectedUser)
    {
        InitializeComponent();
        BindingContext = new EditUserViewModel(selectedUser);
    }
}