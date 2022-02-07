using EmployeeAccounting.Data;
using EmployeeAccounting.Interfaces;
using EmployeeAccounting.Models;

namespace EmployeeAccounting.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public bool Exist(int id)
        {
            return _context.Posts.Any(p => p.Id == id);
        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Post> GetAll()
        {
            return _context.Posts.OrderBy(p => p.Id).ToList();
        }

        public ICollection<Employee> GetEmployeesByPostId(int postId)
        {
            return _context.Employees.Where(e => e.Post.Id == postId).OrderBy(e => e.Id).ToList();
        }

        public bool Create(Post post)
        {
            _context.Add(post);
            return Save();
        }

        public bool Update(Post post)
        {
            _context.Update(post);
            return Save();
        }

        public bool Delete(Post post)
        {
            _context.Remove(post);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
