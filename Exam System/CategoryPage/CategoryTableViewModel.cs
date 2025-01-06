using Exam_System.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static Exam_System.AccountPages.LoginViewModel;

namespace Exam_System.CategoryPage
{
    public class CategoryTableViewModel
    {
        private readonly ApiService _api;
        public ObservableCollection<Category> Categories { get; set; }
        public bool IsAdmin => UserSession.IsAdmin;
        public Command CreateCommand { get; }
        public Command<Category> EditCommand { get; }
        public Command<Category> RemoveCommand { get; }
        public CategoryTableViewModel()
        {
            _api = new ApiService();
            Categories = new ObservableCollection<Category>();
            LoadCategory();
            CreateCommand = new Command(OnCreateClicked);
            RemoveCommand = new Command<Category>(OnRemoveClicked);
            EditCommand = new Command<Category>(OnEditlicked);
        }

        private void OnEditlicked(Category category)
        {
            App.Current.MainPage.Navigation.PushAsync(new CreateCategory(category));
        }

        private async void OnRemoveClicked(Category category)
        {
            var result = await _api.DeleteAsync<ApiResponse>($"Category/{category.Id}");
        }

        private async void LoadCategory()
        {
            var categorys = await _api.GetAsync<List<Category>>("Category");
            foreach (var category in categorys)
            {
                category.Num_Of_Q = category.Questions.Count();
                Categories.Add(category);
            }
        }

        private async void OnCreateClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateCategory(null));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
