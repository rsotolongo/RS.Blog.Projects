using System.Web.Http;

namespace RS.Blog.Projects.SigningAssemblies.FullAPI.Controllers
{
    public class FirstController : ApiController
    {
        [HttpGet, Route("api/demo/string")]
        public string StringMethod()
        {
            return nameof(this.StringMethod);
        }
    }
}
