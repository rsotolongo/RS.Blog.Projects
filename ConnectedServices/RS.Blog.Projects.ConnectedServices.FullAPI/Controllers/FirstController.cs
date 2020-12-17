using System.Web.Http;

namespace RS.Blog.Projects.ConnectedServices.FullAPI.Controllers
{
    public class FirstController : ApiController
    {
        [HttpGet, Route("api/first/number")]
        public int NumberMethod()
        {
            return 111;
        }

        [HttpGet, Route("api/first/string")]
        public string StringMethod()
        {
            return "Framework: Full, Controller: First, Method: String";
        }
    }
}
