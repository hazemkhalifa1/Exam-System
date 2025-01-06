using BLL.Interface;
using DAL.Entities;
using DAL.Model;
using Exam.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Exam
        [HttpGet]
        public async Task<IEnumerable<DAL.Entities.Exam>> Get() => await _unitOfWork.ExamRepostory.GetAll();

        // GET api/Exam/Examenr/5
        [HttpGet("Examenr/{id}")]
        public IEnumerable<ExamenrVM> GetExamenr(Guid id) => _unitOfWork.ExamRepostory.GetExamenr(id).Select(u => new ExamenrVM
        {
            Name = u.user.UserName,
            Id = u.UserId,
            Degree = u.ExamResult
        });

        [HttpPost("Examenr")]
        public IActionResult PostExamenr(UserExamVM userExam)
        {
            UserExam UserExam = new UserExam { ExamId = userExam.ExamId, UserId = userExam.UserId, ExamResult = userExam.Degree };
            _unitOfWork.ExamRepostory.AddUserExam(UserExam);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        // GET api/<ExamController>/Questions/5
        [HttpGet("Questions/{id}")]
        public IEnumerable<Question> GetQuestions(Guid id) => _unitOfWork.ExamRepostory.GetQuestions(id);

        // POST api/<ExamController>
        [HttpPost]
        public void Post([FromBody] ExamInput input)
        {
            Random random = new Random();
            List<Question> questions = new List<Question>();
            decimal total = 0;
            foreach (var item in input.CategoryList)
            {
                total += (item.DegreePerQuestion * item.NumberOfQuestions);
                var cat = _unitOfWork.categoryRepostory.GetByName(item.CategoryName).Result.Questions.Where(q => q.Degree == item.DegreePerQuestion);
                questions.AddRange(cat.OrderBy(x => random.Next()).Take(item.NumberOfQuestions));
            }
            DAL.Entities.Exam exam = new DAL.Entities.Exam
            {
                Total_Degree = total,
                Questions = questions,
                Name = input.Name,
            };
            _unitOfWork.ExamRepostory.Create(exam);
            _unitOfWork.SaveChanges();
        }

        // PUT api/<ExamController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] UserExam userExam)
        {
            _unitOfWork.ExamRepostory.Update(userExam);
            _unitOfWork.SaveChanges();
        }

        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _unitOfWork.ExamRepostory.Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
