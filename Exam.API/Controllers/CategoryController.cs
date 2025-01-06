using AutoMapper;
using BLL.Interface;
using DAL.Entities;
using Exam.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cat = await _unitOfWork.categoryRepostory.GetAll();
            return Ok(cat);
        }

        // GET: api/Category/Names
        [HttpGet("Names")]
        public IEnumerable<string> GetName() => _unitOfWork.categoryRepostory.GetAllNames();

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<Category> Get([FromForm] Guid id) => await _unitOfWork.categoryRepostory.GetById(id);

        // GET api/<CategoryController>/Questions/5
        [HttpGet("Questions/{id}")]
        public IEnumerable<Question> GetQuestions(Guid id) => _unitOfWork.categoryRepostory.GetQuestions(id);

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] CategoryModel category)
        {
            var _category = new Category()
            {
                Name = category.Name,
                Questions = _mapper.Map<List<Question>>(category.Questions)
            };
            _unitOfWork.categoryRepostory.Create(_category);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category category)
        {
            _unitOfWork.categoryRepostory.Update(category);
            _unitOfWork.SaveChanges();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _unitOfWork.categoryRepostory.Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
