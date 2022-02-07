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

        public bool Create(Department department)
        {
            _context.Add(department);
            return Save();
        }

        public bool Delete(Department department)
        {
            _context.Remove(department);
            return Save();
        }

        public bool Exist(int id)
        {
            return _context.Departments.Any(d => d.Id == id);
        }

        public Department GetById(int id)
        {
            return _context.Departments.Where(d => d.Id == id).FirstOrDefault();
        }

        public ICollection<Department> GetAll()
        {
            return _context.Departments.OrderBy(d => d.Id).ToList();
        }

        public ICollection<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            return _context.Employees.Where(e => e.Department.Id == departmentId).OrderBy(e => e.Id).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(Department department)
        {
            _context.Update(department);
            return Save();
        }
    }
}
