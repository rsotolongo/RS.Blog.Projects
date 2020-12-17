using System.Web.Http;

namespace RS.Blog.Projects.ConnectedServices.FullAPI.Controllers
{
    public class SecondController : ApiController
    {
        [HttpGet, Route("api/second/number")]
        public int NumberMethod()
        {
            return 122;
        }

        [HttpGet, Route("api/second/string")]
        public string StringMethod()
        {
            return "Framework: Full, Controller: Second, Method: String";
        }
    }
}
