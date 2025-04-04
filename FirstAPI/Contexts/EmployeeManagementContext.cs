using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Contexts
{
    public class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }


        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.Department_Id);

            modelBuilder.Entity<Salary>().HasMany(s => s.EmployeeSalaries)
                .WithOne(es => es.Salary)
                .HasForeignKey(es => es.SalaryId);

            modelBuilder.Entity<EmployeeSalary>().HasKey(es => es.SalaryEmployeeId);

            modelBuilder.Entity<EmployeeSalary>()
                .HasOne(es => es.Employee)
                .WithMany(e => e.EmployeeSalaries)
                .HasForeignKey(es => es.EmployeeId);
        }
    }
}
