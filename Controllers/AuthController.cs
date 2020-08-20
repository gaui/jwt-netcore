using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route ("auth")]
public class AuthController : ControllerBase {
  private IConfiguration Configuration { get; }

  public AuthController (IConfiguration configuration) {
    Configuration = configuration;
  }

  [HttpGet("token")]
  public IActionResult GetToken () {
    var jwt = new JwtService (Configuration);
    var token = jwt.GenerateToken ("my@email.com");
    return Ok(token);
  }
}
