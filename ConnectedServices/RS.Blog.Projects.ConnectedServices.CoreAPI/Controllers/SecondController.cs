using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RS.Blog.Projects.ConnectedServices.CoreAPI.Controllers
{
    public class SecondController : ControllerBase
    {
        [HttpGet, Route("api/second/number")]
        [SwaggerOperation(OperationId = "Second_NumberMethod")]
        public int NumberMethod()
        {
            return 222;
        }

        [HttpGet, Route("api/second/string")]
        [SwaggerOperation(OperationId = "Second_StringMethod")]
        public string StringMethod()
        {
            return "Framework: Core, Controller: Second, Method: String";
        }
    }
}
