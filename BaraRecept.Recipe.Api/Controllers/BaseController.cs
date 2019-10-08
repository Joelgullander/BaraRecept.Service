using Microsoft.AspNetCore.Mvc;

namespace BaraRecept.Recipe.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// BaseController from which all API-controllers inherit
    /// </summary>
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
    }
}
