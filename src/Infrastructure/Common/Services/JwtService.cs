﻿using DigitalWallet.Application.Interfaces.Services;
using DigitalWallet.Application.Models;
using DigitalWallet.Infrastructure.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalWallet.Infrastructure.Common.Services
{
    public class JwtService : IJwtService
    {
        #region constructor
        private readonly ILogger<JwtService> logger;

        public JwtService(ILogger<JwtService> _logger)
        {
            logger = _logger;
        }
        #endregion

        public (Result Status, string Token) GenerateToken(List<Claim> claims, DateTime? expire)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(JwtConfig.secretKey);
                var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.encryptionKey)),
                    SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature),
                    EncryptingCredentials = encryptingCredentials
                };
                if (expire.HasValue)
                    tokenDescriptor.Expires = expire.Value;
                return (Result.Ok(), tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return (Result.Fail(), null);
            }
        }
    }
}