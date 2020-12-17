using Microsoft.AspNetCore.Mvc;

namespace RS.Blog.Projects.SigningAssemblies.CoreAPI.Controllers
{
    public class DemoController : ControllerBase
    {
        [HttpGet, Route("api/demo/string")]
        public string StringMethod()
        {
            return nameof(this.StringMethod);
        }
    }
}
