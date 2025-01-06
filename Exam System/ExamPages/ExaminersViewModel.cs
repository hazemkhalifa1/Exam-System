using Exam_System.Models;
using System.Collections.ObjectModel;

namespace Exam_System.ExamPages
{
    public class ExaminersViewModel
    {
        private readonly ApiService _api;
        private Guid _examId;
        public ObservableCollection<Examiner> Examiners { get; set; }

        public ExaminersViewModel(Guid ExamId)
        {
            _examId = ExamId;
            _api = new ApiService();
            LoadExaminer();
        }

        private async void LoadExaminer()
        {
            var examiner = await _api.GetAsync<IEnumerable<Examiner>>($"Exam/Examenr/{_examId}");
            foreach (var item in examiner)
            {
                Examiners.Add(item);
            }
        }
    }
}
