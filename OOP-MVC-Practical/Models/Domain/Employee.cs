namespace OOP_MVC_Practical.Models.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Age { get; set; }
        public List<Department> Departments { get; set; }
    }
}
