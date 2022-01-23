using EmployeeAccounting.Models;

namespace EmployeeAccounting.Interfaces
{
    public interface IPostRepository
    {
        ICollection<Post> GetPosts();
        Post GetPost(int id);
        ICollection<Employee> GetEmployeesByPost(int postId); 
        bool PostExist(int id);
        bool CreatePost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(Post post);
        bool Save();
    }
}
