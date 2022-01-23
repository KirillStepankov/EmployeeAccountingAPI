using EmployeeAccounting.Data;
using EmployeeAccounting.Interfaces;
using EmployeeAccounting.Models;

namespace EmployeeAccounting.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateDepartment(Department department)
        {
            _context.Add(department);
            return Save();
        }

        public bool DeleteDepartment(Department department)
        {
            _context.Remove(department);
            return Save();
        }

        public bool DepartmentExist(int id)
        {
            return _context.Departments.Any(d => d.Id == id);
        }

        public Department GetDepartment(int id)
        {
            return _context.Departments.Where(d => d.Id == id).FirstOrDefault();
        }

        public ICollection<Department> GetDepartments()
        {
            return _context.Departments.OrderBy(d => d.Id).ToList();
        }

        public ICollection<Employee> GetEmployeesByDepartment(int departmentId)
        {
            return _context.Employees.Where(e => e.Department.Id == departmentId).OrderBy(e => e.Id).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateDepartment(Department department)
        {
            _context.Update(department);
            return Save();
        }
    }
}
