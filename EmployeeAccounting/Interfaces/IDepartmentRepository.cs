using EmployeeAccounting.Models;

namespace EmployeeAccounting.Interfaces
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetDepartments();
        Department GetDepartment(int id);
        ICollection<Employee> GetEmployeesByDepartment(int postId); 
        bool DepartmentExist(int id);
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(Department department);
        bool Save();
    }
}
