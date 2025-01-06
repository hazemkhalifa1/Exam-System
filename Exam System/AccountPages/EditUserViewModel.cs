using System.Collections.ObjectModel;
using static Exam_System.AccountPages.LoginViewModel;

namespace Exam_System.AccountPages
{
    public class EditUserViewModel : BaseViewModel
    {
        private readonly ApiService _api;
        public UserDto SelectedUser { get; set; }
        public ObservableCollection<string> Roles { get; set; }

        public Command SaveChangesCommand { get; }

        public EditUserViewModel()
        {
            _api = new ApiService();
            Roles = new ObservableCollection<string> { "User", "Admin" };
            SaveChangesCommand = new Command(OnSaveChanges);
        }
        public EditUserViewModel(UserDto user) : this()
        {
            SelectedUser = user;
        }

        private async void OnSaveChanges()
        {
            if (SelectedUser.Username is null)
            {
                App.Current.MainPage.DisplayAlert("خطاء", "ادخل اسم المستخدم", "موافق");
            }
            else if (SelectedUser.Email is null)
            {
                App.Current.MainPage.DisplayAlert("خطاء", "ادخل الايميل", "موافق");
            }
            else if (SelectedUser.Role is null)
            {
                App.Current.MainPage.DisplayAlert("خطاء", "ادخل الصلاحيه", "موافق");
            }

            var response = await _api.PutAsync<ApiResponse>($"Account/EditUser", SelectedUser);
            App.Current.MainPage.DisplayAlert("ناجح", "تم انشاء قسم بنجاح", "OK");

        }
    }
}