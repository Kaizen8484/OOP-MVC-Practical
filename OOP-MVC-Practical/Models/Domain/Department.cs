namespace OOP_MVC_Practical.Models.Domain
{
    public class Department
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string Designation { get; set; }
        public Employee Employee { get; set; }
    }
}
