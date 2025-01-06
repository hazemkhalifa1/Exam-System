using Exam_System.Models;
using System.Windows.Input;

namespace Exam_System.ExamPages
{
    public class QuestionViewModel : BaseViewModel
    {
        private readonly ApiService _api;
        private string _answer;
        private string _rightAnswer;
        private string _body;
        private string _buttonText;
        private decimal _degree;
        private decimal _totalDegree;

        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);
        }

        public string Body
        {
            get => _body;
            set => SetProperty(ref _body, value);
        }

        public string Answer
        {
            get => _answer;
            set => SetProperty(ref _answer, value);
        }

        public string RightAnswer
        {
            get => _rightAnswer;
            set => SetProperty(ref _rightAnswer, value);
        }

        public ICommand NextCommand { get; }

        public QuestionViewModel()
        {
            _api = new ApiService();
            NextCommand = new Command(OnNextQuestion);
        }

        public QuestionViewModel(Question question, ref decimal totalDegree, string ButtonText) : base()
        {
            _buttonText = ButtonText;
            _body = question.Body;
            _rightAnswer = question.Answer;
            _degree = question.Degree;
            _totalDegree = totalDegree;
        }

        private async void OnNextQuestion()
        {
            if (Answer.Equals(RightAnswer))
            {
                _totalDegree += _degree;
            }
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
