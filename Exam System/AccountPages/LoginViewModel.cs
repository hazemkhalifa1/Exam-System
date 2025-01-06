using System.Windows.Input;

namespace Exam_System.AccountPages
{
    public class LoginViewModel : BaseViewModel
    {
        public static string UserId;
        private readonly ApiService _api;
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool _isErrorVisible;
        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => SetProperty(ref _isErrorVisible, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public LoginViewModel()
        {
            _api = new ApiService();
            LoginCommand = new Command(OnLogin);
            NavigateToRegisterCommand = new Command(OnNavigateToRegister);
        }

        private async void OnLogin()
        {
            IsErrorVisible = false;

            // Example validation
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Username and Password are required.";
                IsErrorVisible = true;
                return;
            }

            // Perform API call or login logic here

            var result = await _api.PostAsync<LoginResponse>("Account/Login", new { Username = _username, Password = _password });
            // تحقق من الرد
            if (result.Success)
            {
                await App.Current.MainPage.DisplayAlert("نجح", result.Message, "OK");
                UserSession.IsAdmin = result.IsAdmin;
                UserSession.UserId = result.UserId;
                UserSession.UserName = _username;
                // Example navigation:
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("خطاء", result.Message, "OK");
            }

        }

        private void OnNavigateToRegister()
        {
            App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
        public class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        private class LoginResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string UserId { get; set; }
            public bool IsAdmin { get; set; }
        }

        public static class UserSession
        {
            public static string UserId { get; set; }
            public static bool IsAdmin { get; set; }
            public static string UserName { get; set; } // يمكنك إضافة بيانات أخرى إذا احتجت
        }

    }
}
