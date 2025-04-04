using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetEmployees(int departmentId)
        {
            try
            {
                var employees = await _employeeService.GetEmployeesByDepartment(departmentId);
                return Ok(employees);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateEmployeeResponse>> AddEmployee(CreateEmployeeRequest request)
        {
            try
            {
                var result = await _employeeService.AddEmployee(request);
                return Created("",result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost("EmployeeSearch")]
        public async Task<ActionResult<IEnumerable<GetEmployeeResponse>>> GetEmployeesByFilter([FromBody] EmployeeRequest request)
        {
            try
            {
                var employees = await _employeeService.GetEmployeesByFilter(request);
                return Ok(employees);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult<IEnumerable<GetEmployeeResponse>>> UpdateEmployee([FromBody] UpdateEmployeeRequest updateEmployeeRequest)
        {
            try
            {
                var employees = await _employeeService.UpdateEmployeeDetails(updateEmployeeRequest);
                return Ok(employees);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
