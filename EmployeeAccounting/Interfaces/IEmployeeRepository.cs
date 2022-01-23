using EmployeeAccounting.Models;

namespace EmployeeAccounting.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();
        Employee GetEmployee(int id);
        ICollection<Employee> GetEmployeesByDepartment(int departmentId);
        ICollection<Employee> GetEmployeesByPost(int postId);
        bool EmployeeExist(int id);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
        bool Save();
    }
}
