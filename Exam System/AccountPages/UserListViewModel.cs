using System.Collections.ObjectModel;

namespace Exam_System.AccountPages
{
    public class UserListViewModel : BaseViewModel
    {
        private readonly ApiService _api;
        public ObservableCollection<UserDto> Users { get; set; } = new ObservableCollection<UserDto>();
        public Command<UserDto> EditCommand { get; }


        public UserListViewModel()
        {
            _api = new ApiService();
            GetUsersFromAPI();
            EditCommand = new Command<UserDto>(OnEditlicked);
        }

        private void OnEditlicked(UserDto user)
        {
            App.Current.MainPage.Navigation.PushAsync(new EditUserPage(user));
        }


        private async void GetUsersFromAPI()
        {
            var users = await _api.GetAsync<List<UserDto>>("Account/All");
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
