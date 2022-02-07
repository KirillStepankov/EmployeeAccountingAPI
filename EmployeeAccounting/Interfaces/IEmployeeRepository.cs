using EmployeeAccounting.Models;

namespace EmployeeAccounting.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        ICollection<Employee> GetByDepartmentId(int departmentId);
        ICollection<Employee> GetByPostId(int postId);
    }
}
