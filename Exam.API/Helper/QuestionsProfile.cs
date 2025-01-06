using AutoMapper;
using DAL.Entities;
using Exam.API.Models;

namespace Exam.API.Helper
{
    public class QuestionsProfile : Profile
    {
        public QuestionsProfile()
        {
            CreateMap<QuestionModel, Question>();
        }
    }
}
