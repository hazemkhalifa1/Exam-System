using Exam_System.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Exam_System.ExamPages
{
    public enum LevelsDegree { A = 3, B = 2, C = 1 };
    public class CreateExamViewModel : BaseViewModel
    {
        private string _examName;
        public string ExamName
        {
            get => _examName;
            set => SetProperty(ref _examName, value);
        }

        public ObservableCollection<Category> Categories { get; set; }
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }
        public ObservableCollection<ExamCategoryInput> SelectedCategores { get; set; }
        private readonly ApiService _api;
        private int _numOfQ;
        public int NumOfQ
        {
            get => _numOfQ;
            set => SetProperty(ref _numOfQ, value);
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
        public Command AddCategoryCommand { get; }
        public Command<ExamCategoryInput> RemoveCategoryCommand { get; }
        public Command SubmitCommand { get; }
        public CreateExamViewModel()
        {
            _api = new ApiService();
            Categories = new ObservableCollection<Category>();
            SelectedCategores = new ObservableCollection<ExamCategoryInput>();
            LoadCategory();
            Levels = new ObservableCollection<string>(Enum.GetNames(typeof(LevelsDegree)));
            AddCategoryCommand = new Command(OnAddCategory);
            //RemoveCategoryCommand = new Command<ExamCategoryInput>(OnRemoveCategory);
            SubmitCommand = new Command(OnSubmet);
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

        //private void OnRemoveCategory(ExamCategoryInput input)
        //{
        //    SelectedCategores.Remove(input);
        //}

        private void OnAddCategory()
        {
            _selectedCategory = SelectedCategory;
            _numOfQ = NumOfQ;
            _selectedLevel = SelectedLevel;
            if (_selectedCategory is null)
            {
                App.Current.MainPage.DisplayAlert("خطاء", "اختر القسم", "OK");
            }
            else if (_numOfQ <= 0)
            {
                App.Current.MainPage.DisplayAlert("خطاء", "ادخل عدد الاسأله", "OK");
            }
            else
            {
                var degree = GetSelectedLevelValue();
                ExamCategoryInput input = new ExamCategoryInput
                {
                    CategoryName = _selectedCategory.Name,
                    NumberOfQuestions = _numOfQ,
                    Questions = _selectedCategory.Questions.Where(Q => Q.Degree == degree).ToList(),
                    DegreePerQuestion = degree
                };
                SelectedCategores.Add(input);
                NumOfQ = 0;
            }

        }


        public async void OnSubmet()
        {
            if (!String.IsNullOrWhiteSpace(_examName))
            {
                Exam exam = new Exam()
                {
                    Name = _examName,
                    Total_Degree = 0
                };
                if (SelectedCategores.Count <= 0)
                {
                    App.Current.MainPage.DisplayAlert("خطاء", "لا يمكن انشاء امتحان فارغ", "OK");
                }
                else
                {
                    foreach (ExamCategoryInput item in SelectedCategores)
                    {
                        exam.Questions.AddRange(item.Questions.OrderBy(q => new Random()).Take(item.NumberOfQuestions));
                        exam.Total_Degree += (item.DegreePerQuestion * item.NumberOfQuestions);
                    }
                }
                var response = await _api.PostAsync<Category>($"Exam", exam);
                App.Current.MainPage.DisplayAlert("ناجح", "تم انشاء امتحان بنجاح", "OK");
            }
            else
                App.Current.MainPage.DisplayAlert("خطاء", "ادخل اسم الامتحان", "OK");
        }
    }
}
