using FirstAPI.Interfaces;
using FirstAPI.Misc;
using FirstAPI.Models.DTOs;
using FirstAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        private readonly ILogger<SalaryController> _logger;

        public SalaryController(ISalaryService salaryService, ILogger<SalaryController> logger)
        {
            _salaryService = salaryService;
            _logger = logger;
        }
        [HttpGet]
        [CustomExceptionFilter]
        public async Task<ActionResult<IEnumerable<SalaryResponse>>> GetAllSalaries()
        {
            var salaries = await _salaryService.GetAllSalaries();
            return Ok(salaries);
        }
        [HttpPost]
        [CustomExceptionFilter]
        public async Task<ActionResult<AssignSalaryResponse>> AssignSalaryToEmployee([FromBody] EmployeeSalaryRequest salaryRequest)
        {
            if (salaryRequest == null)
            {
                _logger.LogError("Salary request is null");
                return BadRequest("Salary request is null");
            }
            var result = await _salaryService.AssignSalaryToEmployee(salaryRequest);
            return Ok(result);
        }
    }
}
