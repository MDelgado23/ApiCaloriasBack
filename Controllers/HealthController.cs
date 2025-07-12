using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("API viva ✅");
}
