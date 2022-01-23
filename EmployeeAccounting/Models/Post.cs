namespace EmployeeAccounting.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
