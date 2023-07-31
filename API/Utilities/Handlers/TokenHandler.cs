﻿using API.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utilities.Handlers;

public class TokenHandler : ITokenHandler
{
    public readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? GenerateToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTConfig:SecretKey"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(issuer: _configuration["JwtConfig:Issuer"],
                                                audience: _configuration["JwtConfig:Audiance"],
                                                claims: claims,
                                                expires: DateTime.Now.AddMinutes(10),
                                                signingCredentials: signinCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }
}
