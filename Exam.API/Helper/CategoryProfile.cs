using AutoMapper;
using DAL.Entities;
using Exam.API.Models;

namespace Exam.API.Helper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>();
        }
    }
}
