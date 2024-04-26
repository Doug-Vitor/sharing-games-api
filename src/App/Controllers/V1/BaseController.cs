using Asp.Versioning;

namespace App.Controllers.V1;

[ApiVersion(Constants.LatestApiVersion)]
public abstract class BaseController : VersionedBaseController;