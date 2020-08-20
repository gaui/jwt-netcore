using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtService {
  private IConfiguration Configuration { get; }

  public JwtService (IConfiguration config) {
    Configuration = config;
  }

  public string GenerateToken (string email) {
    var tokenHandler = new JwtSecurityTokenHandler ();
    var key = Encoding.ASCII.GetBytes (Configuration["JwtConfig:Secret"]);
    var tokenDescriptor = new SecurityTokenDescriptor {
      Issuer = Configuration["JwtConfig:Issuer"],
      Audience = Configuration["JwtConfig:Audience"],
      Subject = new ClaimsIdentity (new [] {
        new Claim (JwtRegisteredClaimNames.Email, email)
      }),
      Expires = DateTime.UtcNow.AddMinutes (int.Parse(Configuration["JwtConfig:ExpirationInMinutes"])),
      SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken (tokenDescriptor);

    return tokenHandler.WriteToken (token);

  }
}
