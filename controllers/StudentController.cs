//using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using CollegeManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;




namespace CollegeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repo;
        private readonly ILogger<StudentController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;



        public StudentController(IStudentRepository repo, ILogger<StudentController> logger, AppDbContext context, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsAsync()
        {
            _logger.LogInformation("The GetStudents method executed");

            var students = await _repo.GetStudentsAsync();
            var stu = _mapper.Map<IEnumerable<StudentDTO>>(students);
            //    {
            //         id = s.id,
            //         name = s.name,
            //         email = s.email

            //    });
            return Ok(stu);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> GetByIdAsync(int id)
        {
            var stu = await _repo.GetByIdAsync(id);
            _logger.LogInformation("The GetById method executed");
            if (stu == null)
            {
                return BadRequest();
            }
            var stuDTO = _mapper.Map<StudentDTO>(stu);
            // {
            //     id = stu.id,
            //     name = stu.name,
            //     email = stu.email
            // };
            return Ok(stuDTO);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody] StudentDTO model)
        {
            _logger.LogInformation("The CreateStudent method executed");

            if (model == null)
            {
                return BadRequest();
            }
            var student = new Student
            {
                name = model.name,
                email = model.email
            };
            var res = await _repo.CreateAsync(student);
            model.id = res.id;

            return Ok(model);
        }
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> UpdateStudentAsync([FromBody] StudentDTO model)
        {
            _logger.LogInformation("The UpdateStudent method executed");

            var updated = _mapper.Map<Student>(model);
            // id = model.id,
            // name = model.name,
            // email = model.email
            // };
            var res = await _repo.UpdateAsync(updated);
            if (res == null)
            {
                return BadRequest();
            }

            return NoContent();
        }
        [HttpPatch]
        [Route("{id:int}/updatePartial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> UpdatePartialAsync(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            var existing = await _context.Students.FirstOrDefaultAsync(n => n.id == id);
            if (existing == null)
            {
                return BadRequest();
            }
            var stuDTO = _mapper.Map<StudentDTO>(existing);
            // {
            //     id = existing.id,
            //     name = existing.name,
            //     email = existing.email
            // };
            patchDocument.ApplyTo(stuDTO);
            existing.id = stuDTO.id;
            existing.name = stuDTO.name;
            existing.email = stuDTO.email;

            return NoContent();
        }
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteStudentAsync(int id)
        {
            _logger.LogInformation("The DeleteStudent method executed");

            var res = await _repo.DeleteStudentAsync(id);
            if (res == false)
            {
                return BadRequest();
            }
            return res;
        }
    }
}