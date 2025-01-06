using Exam_System.Models;
using System.Collections.ObjectModel;
using static Exam_System.AccountPages.LoginViewModel;

namespace Exam_System.ExamPages
{
    public class ExamsTableViewModel : BaseViewModel
    {
        private readonly ApiService _api;

        public ObservableCollection<Exam> Exams { get; set; }
        public Command<Exam> ExaminerCommand { get; }
        public Command<Exam> StartCommand { get; }
        public Command<Exam> RemoveCommand { get; }
        public Command CreateCommand { get; }

        // خاصية لتحديد إذا كان المستخدم مسؤولاً
        public bool IsAdmin => UserSession.IsAdmin;

        public ExamsTableViewModel()
        {
            // تعيين صلاحيات المسؤول (يمكنك تعديلها بناءً على حالة تسجيل الدخول)
            _api = new ApiService();
            Exams = new ObservableCollection<Exam>();
            // بيانات افتراضية
            LoadExams();
            ExaminerCommand = new Command<Exam>(OnExaminerClicked);
            StartCommand = new Command<Exam>(OnStartClicked);
            RemoveCommand = new Command<Exam>(OnRemoveClicked);
            CreateCommand = new Command(OnCreateClicked);
        }

        private async void LoadExams()
        {
            var exams = await _api.GetAsync<List<Exam>>("Exam");
            foreach (var exam in exams)
            {
                Exams.Add(exam);
            }
        }

        private async void OnExaminerClicked(Exam exam)
        {
            // منطق المسؤول
            await Application.Current.MainPage.Navigation.PushAsync(new ExaminerPage(exam.Id));
        }
        private async void OnRemoveClicked(Exam exam)
        {
            var response = await _api.DeleteAsync<ApiResponse>($"Exam/{exam.Id}");
            if (response.Success)
                Application.Current.MainPage.DisplayAlert("ناجح", response.Message, "موافق");
            else
                Application.Current.MainPage.DisplayAlert("خطاء", response.Message, "موافق");

        }

        private async void OnStartClicked(Exam exam)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new QuestionPage(exam));
        }

        private async void OnCreateClicked()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateExam());
        }

    }
}
