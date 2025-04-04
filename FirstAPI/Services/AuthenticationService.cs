using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;
using FirstAPI.Repositories;
using System.Security.Cryptography;

namespace FirstAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IRepository<string,User> userRepository,
                                    IRepository<int,Employee> employeeRepository,
                                    ITokenService tokenService) 
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _tokenService = tokenService;

        }
        public async Task<LoginResponse> Login(UserLoginRequest loginRequest)
        {
            var user = await _userRepository.GetById(loginRequest.Username);
            HMACSHA512 hMACSHA512 = new HMACSHA512(user.HashKey);
            var userPassword = hMACSHA512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginRequest.Password));
            for (int i = 0; i < userPassword.Length; i++)
            {
                if (userPassword[i] != user.Password[i])
                    throw new UnauthorizedAccessException("Invalid password");
            }
            var employee = (await _employeeRepository.GetAll()).FirstOrDefault(e => e.Phone == loginRequest.Username);
            if (employee == null)
                throw new UnauthorizedAccessException("User not found");
            var token = await _tokenService.GenerateToken(employee.Id, employee.Name);
            return new LoginResponse { Id = employee.Id,Name=employee.Name,Token=token };
        }
    }
}
