using EmployeeAccounting.Data;
using EmployeeAccounting.Models;

namespace EmployeeAccounting
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Employees.Any())
            {
                var employees = new List<Employee>()
                {
                    new Employee()
                    {
                        FIO = "Иванов Иван Иванович",
                        DateAdded = new DateTime(2022,01,22),
                        DateModified = new DateTime(2022,01,22),
                        DateEmployment = new DateTime(2022,01,22),
                        Department = new Department
                        {
                            Name = "Администрация",
                            DateAdded = new DateTime(2022, 01, 22),
                            DateModified= new DateTime(2022, 01, 22)
                        },
                        Post = new Post
                        {
                            Name = "Директор"
                        }
                    },
                    new Employee()
                    {
                        FIO = "Петров Петр Петрович",
                        DateAdded = new DateTime(2022,01,22),
                        DateModified = new DateTime(2022,01,22),
                        DateEmployment = new DateTime(2022,01,22),
                        Department = new Department
                        {
                            Name = "Разработка",
                            DateAdded = new DateTime(2022, 01, 22),
                            DateModified= new DateTime(2022, 01, 22)
                        },
                        Post = new Post
                        {
                            Name = "Программист"
                        }
                    }
                };

                dataContext.Employees.AddRange(employees);
                dataContext.SaveChanges();
            }
        }
    }
}