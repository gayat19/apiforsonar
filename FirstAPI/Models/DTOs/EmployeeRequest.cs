namespace FirstAPI.Models.DTOs
{
    public class Range
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
    public class EmployeeFilter
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }

        public Range? Age { get; set; }
        public  IEnumerable<int>? Departments { get; set; }
    }
    public class Pagination
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
    public class EmployeeRequest
    {
        public EmployeeFilter? Filters { get; set; }
        public Pagination Pagination { get; set; } = new Pagination();

        // 1 for ascending Id, -1 for descending Id, 2 for ascending Name, -2 for descending Name <summary>
        // 3 for ascending Age, -3 for descending Age, 4 for ascending Department_Id, -4 for descending Department_Id

        public int? SortBy { get; set; } = 1;

    }
}
