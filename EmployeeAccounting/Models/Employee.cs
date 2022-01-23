namespace EmployeeAccounting.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateEmployment { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
