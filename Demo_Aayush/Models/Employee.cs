namespace Demo_code
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DepartmentId { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}
