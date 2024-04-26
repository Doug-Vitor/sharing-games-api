using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase;

[Route("api/v{version:apiVersion}/[controller]")]
public abstract class VersionedBaseController : BaseController;