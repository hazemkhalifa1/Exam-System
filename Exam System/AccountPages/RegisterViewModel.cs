using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using static Exam_System.AccountPages.LoginViewModel;

namespace Exam_System.AccountPages
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _errorMessage;
        private bool _isErrorVisible;

        private readonly ApiService _api;

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await OnRegisterAsync());
            _api = new ApiService(); // Configure the base address if needed
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => SetProperty(ref _isErrorVisible, value);
        }

        public ICommand RegisterCommand { get; }

        private async Task OnRegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await App.Current.MainPage.DisplayAlert("خطاء", "ادخل جميع المعلومات المطلوبه", "OK");
                IsErrorVisible = true;
                return;
            }

            if (Password != ConfirmPassword)
            {
                await App.Current.MainPage.DisplayAlert("خطاء", "الباسورد غير مطابق", "OK");
                IsErrorVisible = true;
                return;
            }

            var registerModel = new
            {
                Username = Username,
                Email = Email,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };
            var response = await _api.PostAsync<ApiResponse>("Account/Register", registerModel);

            if (response.Success)
            {
                await Application.Current.MainPage.DisplayAlert("ناجح", response.Message, "OK");
                // Redirect to Login Page or clear form
                Username = string.Empty;
                Email = string.Empty;
                Password = string.Empty;
                ConfirmPassword = string.Empty;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("خطاء", response.Message, "OK");
                IsErrorVisible = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
