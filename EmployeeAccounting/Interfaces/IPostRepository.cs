using EmployeeAccounting.Models;

namespace EmployeeAccounting.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        ICollection<Employee> GetEmployeesByPostId(int postId);
    }
}
