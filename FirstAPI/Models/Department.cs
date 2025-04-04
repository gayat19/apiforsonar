namespace FirstAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Employee>? Employees { get; set; }//by default it is null
    }
}
