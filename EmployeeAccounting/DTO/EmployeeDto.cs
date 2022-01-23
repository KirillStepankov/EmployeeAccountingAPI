namespace EmployeeAccounting.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; }
        public DateTime DateEmployment { get; set; }
        public DepartmentDto Department { get; set; }
        public PostDto Post { get; set; }
    }
}
