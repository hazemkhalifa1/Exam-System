using Exam_System.ExamPages;
using Exam_System.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Exam_System.CategoryPage
{
    public class CreateCategoryVM : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();
        private readonly ApiService _api;
        private string _body;
        public string Body
        {
            get => _body;
            set => SetProperty(ref _body, value);
        }
        private string _answer;
        public string Answer
        {
            get => _answer;
            set => SetProperty(ref _answer, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Levels { get; }
        private string _selectedLevel;

        public string SelectedLevel
        {
            get => _selectedLevel;
            set
            {
                if (_selectedLevel != value)
                {
                    _selectedLevel = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedLevel)));
                }
            }
        }

        public decimal GetSelectedLevelValue()
        {
            if (Enum.TryParse(typeof(LevelsDegree), SelectedLevel, out var level))
            {
                return (decimal)(LevelsDegree)level;
            }
            return 0;
        }
        public Command AddQuestionCommand { get; }
        public Command SubmitCommand { get; }
        public CreateCategoryVM()
        {
            _api = new ApiService();
            Levels = new ObservableCollection<string>(Enum.GetNames(typeof(LevelsDegree)));
            AddQuestionCommand = new Command(OnAddQuestion);
            SubmitCommand = new Command(OnSubmet);
        }
        public CreateCategoryVM(Category? category) : base()
        {
            _api = new ApiService();
            Levels = new ObservableCollection<string>(Enum.GetNames(typeof(LevelsDegree)));
            AddQuestionCommand = new Command(OnAddQuestion);
            SubmitCommand = new Command(OnSubmet);
            if (category != null)
            {
                _name = category.Name;
                foreach (var q in category.Questions)
                    Questions.Add(q);
            }
        }


        private void OnAddQuestion()
        {
            if (String.IsNullOrWhiteSpace(_body))
            {
                App.Current.MainPage.DisplayAlert("خطاء", "ادخل السؤال", "OK");
            }
            else if (String.IsNullOrWhiteSpace(_answer))
            {
                App.Current.MainPage.DisplayAlert("خطاء", "ادخل الاجابه", "OK");
            }
            else
            {
                Question input = new Question
                {
                    Body = _body,
                    Answer = _answer,
                    Degree = GetSelectedLevelValue()
                };
                Questions.Add(input);
            }
            if (Questions.Count <= 0)
            {
                App.Current.MainPage.DisplayAlert("خطاء", "لا يمكن اضافه سؤال فارغ", "OK");
            }
            else
            {
                Body = null;
                Answer = null;
            }

        }


        public async void OnSubmet()
        {
            if (!String.IsNullOrWhiteSpace(_name))
            {
                Category category = new Category()
                {
                    Name = _name,
                    Questions = Questions.ToList()
                };

                var response = await _api.PostAsync<Category>($"Category", category);
                App.Current.MainPage.DisplayAlert("ناجح", "تم انشاء قسم بنجاح", "OK");
            }
            else
                App.Current.MainPage.DisplayAlert("خطاء", "ادخل اسم القسم", "OK");
        }
    }
}
