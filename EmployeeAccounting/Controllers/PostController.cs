using AutoMapper;
using EmployeeAccounting.DTO;
using EmployeeAccounting.Interfaces;
using EmployeeAccounting.Models;
using EmployeeAccounting.Validators;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAccounting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        public IActionResult GetPosts()
        {
            var posts = _mapper.Map<List<PostDto>>(_postRepository.GetAll());

            if (!ModelState.IsValid)
                BadRequest(ModelState);

            return Ok(posts);
        }

        [HttpGet("{postId}")]
        [ProducesResponseType(200, Type = typeof(Post))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int postId)
        {
            if (!_postRepository.Exist(postId))
                return NotFound();

            var posts = _mapper.Map<PostDto>(_postRepository.GetById(postId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        [HttpGet("{postId}/Employee")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeesByPost(int postId)
        {
            if (!_postRepository.Exist(postId))
                return NotFound();

            var employees = _mapper.Map<EmployeeDto>(_postRepository.GetEmployeesByPostId(postId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePost([FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postMap = _mapper.Map<Post>(postDto);


            if (!_postRepository.Create(postMap))
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
        public IActionResult UpdatePost(int id, [FromBody] PostDto postDto)
        {
            if (postDto == null)
                return BadRequest(ModelState);

            if (id != postDto.Id)
                return BadRequest(ModelState);

            if (!_postRepository.Exist(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var postMap = _mapper.Map<Post>(postDto);

            if (!_postRepository.Update(postMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Post");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePost(int id)
        {
            if (!_postRepository.Exist(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postRepository.Delete(_postRepository.GetById(id)))
            {
                ModelState.AddModelError("", "Something went wrong when deleting post");
            }

            return NoContent();
        }
    }
}
