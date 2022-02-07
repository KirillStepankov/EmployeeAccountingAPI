using EmployeeAccounting.Models;

namespace EmployeeAccounting.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        ICollection<Employee> GetEmployeesByDepartmentId(int departmentId);
    }
}
