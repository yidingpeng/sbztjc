using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RW.PMS.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class RWBaseController : ControllerBase
{
}
