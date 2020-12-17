using System;
using System.Web;
using System.Web.Http;

namespace RS.Blog.Projects.ConnectedServices.FullAPI
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(this.RegisterApi);
        }

        private void RegisterApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}
