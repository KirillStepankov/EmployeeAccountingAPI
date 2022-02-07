using AutoMapper;
using EmployeeAccounting.DTO;
using EmployeeAccounting.Interfaces;
using EmployeeAccounting.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAccounting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPostRepository postRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetAll());

            if(!ModelState.IsValid)
                BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int id)
        {
            if(!_employeeRepository.Exist(id))
                return NotFound();

            var employee = _mapper.Map<EmployeeDto>( _employeeRepository.GetById(id));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);  

            return Ok(employee);
        }

        [HttpGet("ByDepartment/{departmentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeesByDepartment(int departmentId)
        {
            if (!_departmentRepository.Exist(departmentId))
                return NotFound();

            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetByDepartmentId(departmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpGet("ByPost/{departmentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeesByPost(int postId)
        {
            if (!_postRepository.Exist(postId))
                return NotFound();

            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetByPostId(postId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeMap = _mapper.Map<Employee>(employeeDto);
            employeeMap.DateModified = DateTime.Now;


            if (!_employeeRepository.Create(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id)
                return BadRequest(ModelState);

            if (!_employeeRepository.Exist(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var employeeMap = _mapper.Map<Employee>(employeeDto);
            employeeMap.DateModified = DateTime.Now; 

            if (!_employeeRepository.Update(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating employee");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int id)
        {
            if (!_employeeRepository.Exist(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employeeRepository.Delete(_employeeRepository.GetById(id)))
            {
                ModelState.AddModelError("", "Something went wrong when deleting employee");
            }

            return NoContent();
        }
    }
}
