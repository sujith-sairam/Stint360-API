﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using organisation_webapi.dtos.admin;
using Organisation_WebAPI.Dtos.Admin;
using Organisation_WebAPI.Models;
using Organisation_WebAPI.Services.AuthRepo;

namespace Organisation_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(AdminRegisterDto request)
        {
            var response = await _authRepository.Register(
                new Admin { UserName = request.UserName }, request.Password, request.Email
                );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(AdminLoginDto request)
        {
            var response = await _authRepository.Login(request.UserName, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}