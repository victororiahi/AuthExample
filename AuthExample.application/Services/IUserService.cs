using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthExample.application.DTOs;
using AuthExample.domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.application.Services
{
    public interface IUserService
    {
        Task<ObjectResult> CreateUser(UserSignupDTO userSignupDTO);
        Task<User> Login(UserLoginDTO userLoginDTO);
        Task<List<string>> GetRolesForUser(User user);
        Task<List<User>> GetUsers();
    }
}
