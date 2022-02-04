using AutoMapper;
using EmployeeAccounting.DTO;
using EmployeeAccounting.Interfaces;
using EmployeeAccounting.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAccounting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DepartmentDto>))]
        public IActionResult GetDepartment()
        {
            var employees = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());

            if(!ModelState.IsValid)
                BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(DepartmentDto))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int id)
        {
            if(!_departmentRepository.DepartmentExist(id))
                return NotFound();

            var department = _mapper.Map<DepartmentDto>(_departmentRepository.GetDepartment(id));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);  

            return Ok(department);
        }

        [HttpGet("{departmentId}/Employee")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DepartmentDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeesByDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExist(departmentId))
                return NotFound();

            var employees = _mapper.Map<List<EmployeeDto>>(_departmentRepository.GetEmployeesByDepartment(departmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null)
                return BadRequest(ModelState);

            var department = _departmentRepository.GetDepartments()
                .Where(d => d.Name.Trim().ToUpper() == departmentDto.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (department != null)
            {
                ModelState.AddModelError("", "Department already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentDto);
            departmentMap.DateModified = DateTime.Now;


            if (!_departmentRepository.CreateDepartment(departmentMap))
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
        public IActionResult UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
           if (id != departmentDto.Id)
                return BadRequest(ModelState);

            if (!_departmentRepository.DepartmentExist(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var departmentMap = _mapper.Map<Department>(departmentDto);
            departmentMap.DateModified = DateTime.Now; 

            if (!_departmentRepository.UpdateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating department");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int id)
        {
            if (!_departmentRepository.DepartmentExist(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_departmentRepository.DeleteDepartment(_departmentRepository.GetDepartment(id)))
            {
                ModelState.AddModelError("", "Something went wrong when deleting department");
            }

            return NoContent();
        }
    }
}
