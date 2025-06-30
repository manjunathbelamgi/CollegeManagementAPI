//using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;




namespace CollegeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repo;
    private readonly ILogger<StudentController> _logger;

    public StudentController(IStudentRepository repo, ILogger<StudentController> logger)
    {
        _repo = repo;
        _logger = logger;
    }
        [HttpGet]
        [Route("GetStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            var stu = _repo.GetStudents().Select(s => new StudentDTO
           {
               id = s.id,
                name = s.name,
               email = s.email

           });
            return Ok(stu);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetById(int id)
        {
            var stu = _repo.GetById(id);
            _logger.LogInformation("Staring the GetById method");
            if (stu == null)
            {
                return BadRequest();
            }
            var stuDTO = new StudentDTO
            {
                id = stu.id,
                name = stu.name,
                email = stu.email
            };
            return Ok(stuDTO);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var student = new Student
            {
                name = model.name,
                email=model.email
            };
            var res = _repo.Create(student);
            model.id = res.id;
           
            return Ok(model);
        }
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var existing = StudentRepository.students.FirstOrDefault(n => n.id == model.id);
            existing.id = model.id;
            existing.name = model.name;
            existing.email = model.email;

            return NoContent();
        }
        [HttpPatch]
        [Route("{id:int}/updatePartial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> UpdatePartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            var existing = StudentRepository.students.FirstOrDefault(n => n.id == id);
            if (existing == null)
            {
                return BadRequest();
            }
            var stuDTO = new StudentDTO
            {
                id = existing.id,
                name = existing.name,
                email = existing.email
            };
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
        public ActionResult<bool> DeleteStudent(int id)
        {
            var stu = StudentRepository.students.FirstOrDefault(n => n.id == id);
            if (stu == null)
            {
                return BadRequest();
            }
            return Ok(StudentRepository.students.Remove(stu));
        }

    }
}