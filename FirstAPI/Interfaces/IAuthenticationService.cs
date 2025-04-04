using FirstAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace FirstAPI.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(UserLoginRequest loginRequest);
    }
}
