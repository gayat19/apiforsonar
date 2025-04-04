using AutoMapper;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;
using FirstAPI.Repositories;
using System.Security.Cryptography;

namespace FirstAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IRepository<int, Department> _departmentRepository;
        private readonly IRepository<string, User> _userRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<int, Employee> employeeRepository,
                                IRepository<int, Department> departmentRepository,
                                IRepository<string, User> userRepository,
                                IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreateEmployeeResponse> AddEmployee(CreateEmployeeRequest request)
        {
            HMACSHA512 hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password));
            var User = MapEmployeeToUser(request, passwordHash, hmac.Key);
            var userResult = await _userRepository.Add(User);
            if (userResult == null)
                throw new Exception("Failed to create user");
            var employee = MapEmployee(request);
            var employeeResult = await _employeeRepository.Add(employee);
            if (employeeResult == null)
                throw new Exception("Failed to create employee");
            return new CreateEmployeeResponse { Id = employeeResult.Id };
        }

        private Employee MapEmployee(CreateEmployeeRequest request)
        {
            Employee employee = new Employee
            {
                Name = request.Name,
                Phone = request.Phone,
                Department_Id = request.DepartmentId
            };
            return employee;
        }

        private User MapEmployeeToUser(CreateEmployeeRequest request, byte[] passwordHash, byte[] key)
        {
            User user = new User
            {

                Username = request.Phone,
                Password = passwordHash,
                HashKey = key
            };
            return user;
        }

        public async Task<IEnumerable<GetEmployeeResponse>> GetEmployeesByDepartment(int departmentId)
        {
            List<GetEmployeeResponse> employees = new List<GetEmployeeResponse>();
            var department = await _departmentRepository.GetById(departmentId);
            if (department == null)
                throw new Exception("Department not found");
            if (department.Employees.Count() == 0)
                throw new Exception("No employees found in this department");
            foreach (var employee in department.Employees)
            {
                employees.Add(new GetEmployeeResponse
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Phone = employee.Phone,
                    Department = department.Name
                });
            }
            return employees;
        }

        public async Task<IEnumerable<GetEmployeeResponse>> GetEmployeesByFilter(EmployeeRequest request)
        {
            var employees = (await _employeeRepository.GetAll()).ToList();
            if(employees.Count() ==0)
                throw new Exception("No employees found");
            if (request.Filters != null)
                employees = FilterEmployee(request.Filters, employees);
            if(request.SortBy != null)
                employees = SortEmployee((int)request.SortBy, employees);
            if (request.Pagination != null)
                employees = PaginateEmployee(request.Pagination, employees);
            var employeeResponse = _mapper.Map<IEnumerable<GetEmployeeResponse>>(employees).ToList();
            employeeResponse = await PopulateDepartmentName(employeeResponse, employees);
            return employeeResponse;
        }

        private async Task<List<GetEmployeeResponse>> PopulateDepartmentName(List<GetEmployeeResponse> employeeResponse, List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                var department = await _departmentRepository.GetById(employee.Department_Id);
                if (department != null)
                {
                    var employeeResponseItem = employeeResponse.FirstOrDefault(e => e.Id == employee.Id);
                    employeeResponseItem.Department = department.Name;
                }
            }
            return employeeResponse;
        }

        private List<Employee> PaginateEmployee(Pagination pagination, List<Employee> employees)
        {
            if(employees.Count() == 0)
                throw new Exception("No employees found");
            if(employees.Count() <= pagination.PageSize)
                return employees;
            employees = employees.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize).ToList();
            return employees;
        }

        private List<Employee> SortEmployee(int sortBy, List<Employee> employees)
        {
            switch(sortBy)
            {
                case -4:
                    employees = employees.OrderByDescending(e => e.Department_Id).ToList();
                    break;
                case -3:
                    employees = employees.OrderByDescending(e => e.Age).ToList(); 
                    break;
                case -2:
                    employees = employees.OrderByDescending(e => e.Name).ToList(); 
                    break;
                case -1:
                    employees = employees.OrderByDescending(e => e.Id).ToList();
                    break;
                case 1:
                    employees = employees.OrderBy(e => e.Id).ToList();
                    break;
                case 2:
                    employees = employees.OrderBy(e => e.Name).ToList();
                    break;
                case 3:
                    employees = employees.OrderBy(e => e.Age).ToList();
                    break;
                case 4:
                    employees = employees.OrderBy(e => e.Department_Id).ToList();
                    break;
               
            }
            return employees;
        }

        private List<Employee> FilterEmployee(EmployeeFilter filters, List<Employee> employees)
        {
            if(filters.Name != null)
                employees = employees.Where(e => e.Name.ToLower().Contains(filters.Name.ToLower())).ToList();
            if (employees.Count() == 0)
                throw new Exception("No employees found");
            if (filters.Phone != null)
                employees = employees.Where(e => e.Phone.Contains(filters.Phone)).ToList();
            if (employees.Count() == 0)
                throw new Exception("No employees found");
            if(filters.Age != null)
            {
                employees = employees.Where(e => e.Age >= filters.Age.MinValue && e.Age <= filters.Age.MaxValue).ToList();
                if (employees.Count() == 0)
                    throw new Exception("No employees found");
            }
            if (filters.Departments != null)
            {
                employees = employees.Where(e => filters.Departments.Contains(e.Department_Id)).ToList();
                if (employees.Count() == 0)
                    throw new Exception("No employees found");
            }
            return employees;

        }

        public async Task<GetEmployeeResponse> UpdateEmployeeDetails(UpdateEmployeeRequest updateEmployeeRequest)
        {
            string departmentName = string.Empty;
            var employee = await _employeeRepository.GetById(updateEmployeeRequest.EmployeeId);
            if (employee == null)
                throw new Exception("Employee not found");
            if (updateEmployeeRequest.PhoneUpdate != null)
            {
                employee.Phone = updateEmployeeRequest.PhoneUpdate.NewPhoneNumber;
            }
            if (updateEmployeeRequest.AgeUpdate != null)
            {
                employee.Age = updateEmployeeRequest.AgeUpdate.NewAge;
            }
            if (updateEmployeeRequest.DepartmentUpdate != null)
            {
                var department = await _departmentRepository.GetById(updateEmployeeRequest.DepartmentUpdate.NewDepartmentId);
                if (department == null)
                    throw new Exception("Department not found");
                employee.Department_Id = updateEmployeeRequest.DepartmentUpdate.NewDepartmentId;
                departmentName = department.Name;
            }
            var result = await _employeeRepository.Update(updateEmployeeRequest.EmployeeId, employee);
            if (result == null)
                throw new Exception("Failed to update employee");
            var employeeResponse = _mapper.Map<GetEmployeeResponse>(result);
            if(departmentName == string.Empty)
            {
                departmentName = (await _departmentRepository.GetById(employee.Department_Id)).Name;
            }
                
            employeeResponse.Department = departmentName;
            return employeeResponse;
        }
    }
}
