using EmployeeAccounting.Data;
using EmployeeAccounting.Interfaces;
using EmployeeAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAccounting.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public bool Create(Employee employee)
        {
            _context.Add(employee);
            return Save();
        }

        public bool Delete(Employee employee)
        {
            _context.Remove(employee);
            return Save();
        }

        public bool Exist(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Include(e => e.Department).Include(e => e.Post).Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Employee> GetAll()
        {
            return _context.Employees.Include(e => e.Department).Include(e => e.Post).OrderBy(e => e.Id).ToList();
        }
        public ICollection<Employee> GetByDepartmentId(int departmentId)
        {
            return GetAll().Where(e => e.Department.Id == departmentId).OrderBy(e => e.Id).ToList();
        }

        public ICollection<Employee> GetByPostId(int postId)
        {
            return GetAll().Where(e => e.Post.Id == postId).OrderBy(e => e.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;   //  ? true  : false;
        }

        public bool Update(Employee employee)
        {
            _context.Update(employee);
            return Save();
        }
    }
}
