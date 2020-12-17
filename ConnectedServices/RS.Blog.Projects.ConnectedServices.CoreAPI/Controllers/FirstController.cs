using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RS.Blog.Projects.ConnectedServices.CoreAPI.Controllers
{
    public class FirstController : ControllerBase
    {
        [HttpGet, Route("api/first/number")]
        [SwaggerOperation(OperationId = "First_NumberMethod")]
        public int NumberMethod()
        {
            return 211;
        }

        [HttpGet, Route("api/first/string")]
        [SwaggerOperation(OperationId = "First_StringMethod")]
        public string StringMethod()
        {
            return "Framework: Core, Controller: First, Method: String";
        }
    }
}
