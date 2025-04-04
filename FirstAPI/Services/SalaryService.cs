using AutoMapper;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;
using FirstAPI.Repositories;

namespace FirstAPI.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IRepository<int, Salary> _salaryRepository;
        private readonly IRepository<int, EmployeeSalary> _employeeSalaryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SalaryService> _logger;

        public SalaryService(IRepository<int, Employee> employeeRepository,
                             IRepository<int,Salary> salaryRepository,
                             IRepository<int,EmployeeSalary> employeeSalaryRepository,
                             IMapper mapper,
                             ILogger<SalaryService> logger)
        {
            _employeeRepository = employeeRepository;
            _salaryRepository = salaryRepository;
            _employeeSalaryRepository = employeeSalaryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<AssignSalaryResponse> AssignSalaryToEmployee(EmployeeSalaryRequest salaryRequest)
        {
            var employee = await _employeeRepository.GetById(salaryRequest.EmployeeId);
            if (employee == null)
            {
                _logger.LogError($"Employee with id {salaryRequest.EmployeeId} not found");
                throw new Exception($"Employee with id {salaryRequest.EmployeeId} not found");
            }
            var salary = await _salaryRepository.GetById(salaryRequest.SalaryId);
            if (salary == null || salary?.Status != "Active")
            {
                _logger.LogError($"Salary with id {salaryRequest.SalaryId} not found");
                throw new Exception($"Salary with id {salaryRequest.SalaryId} not found");
            }
            var employeeSalary = _mapper.Map<EmployeeSalary>(salaryRequest);
            employeeSalary.AssignedDate = DateTime.Now;
            var result = await _employeeSalaryRepository.Add(employeeSalary);
            if (result == null)
            {
                _logger.LogError("Failed to assign salary to employee");
                throw new Exception("Failed to assign salary to employee");
            }
            var assignSalaryResponse = new AssignSalaryResponse { EmployeeSalaryId = result.SalaryEmployeeId };
            return assignSalaryResponse;
        }

        public async Task<IEnumerable<SalaryResponse>> GetAllSalaries()
        {
            var salaries = await _salaryRepository.GetAll();
            salaries = salaries.Where(s => s.Status == "Active");
            if (salaries.Count() == 0)
            {
                _logger.LogError("No salaries found");
                throw new Exception("No salaries found");
            }
            var salaryResponse = _mapper.Map<IEnumerable<SalaryResponse>>(salaries);
            return salaryResponse;
        }
    }
}
