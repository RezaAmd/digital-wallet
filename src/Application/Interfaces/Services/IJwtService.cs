using Application.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Interfaces.Services
{
    public interface IJwtService
    {
        (Result Status, string Token) GenerateToken(List<Claim> claims, DateTime? expire = default);
    }
}