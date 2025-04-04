namespace FirstAPI.Models.DTOs
{
    public class SalaryResponse
    {
        public int Id { get; set; }
        public float Basic { get; set; }
        public float HRA { get; set; }
        public float DA { get; set; }
        public float Allowance { get; set; }
        public float PF { get; set; }

    }
}
