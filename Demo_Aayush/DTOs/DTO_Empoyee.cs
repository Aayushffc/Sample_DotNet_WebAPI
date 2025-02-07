namespace Demo_Aayush.DTOs
{
    public class DTO_Employee
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; } 
        public decimal Salary { get; set; }
        public DateTime? DateOfJoining { get; set; }
    }
}