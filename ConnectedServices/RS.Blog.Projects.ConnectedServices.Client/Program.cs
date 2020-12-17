using System;
using System.Net.Http;

namespace RS.Blog.Projects.ConnectedServices.Client
{
    internal class Program
    {
        private static void Main()
        {
            var fullClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5200"),
            };
            var operationFullFirstClient = new FullAPI.FirstClient(fullClient);
            int operationFullFirstNUmberResult = operationFullFirstClient.NumberMethodAsync().Result;
            string operationFullFirstStringResult = operationFullFirstClient.StringMethodAsync().Result;
            var operationFullSecondClient = new FullAPI.SecondClient(fullClient);
            int operationFullSecondNumberResult = operationFullSecondClient.NumberMethodAsync().Result;
            string operationFullSecondStringResult = operationFullSecondClient.StringMethodAsync().Result;

            var coreClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5100"),
            };
            var operationCoreFirstClient = new CoreAPI.FirstClient(coreClient);
            int operationCoreFirstNumberResult = operationCoreFirstClient.NumberMethodAsync().Result;
            string operationCoreFirstStringResult = operationCoreFirstClient.StringMethodAsync().Result;
            var operationCoreSecondClient = new CoreAPI.SecondClient(coreClient);
            int operationCoreSecondNumberResult = operationCoreSecondClient.NumberMethodAsync().Result;
            string operationCoreSecondStringResult = operationCoreSecondClient.StringMethodAsync().Result;

            //// To generate clients based on the routes.
            /*
            var pathFullFirstClient = new PathFullAPI.FirstClient(fullClient);
            int pathFullFirstNUmberResult = pathFullFirstClient.NumberAsync().Result;
            string pathFullFirstStringResult = pathFullFirstClient.StringAsync().Result;
            var pathFullSecondClient = new PathFullAPI.SecondClient(fullClient);
            int pathFullSecondNumberResult = pathFullSecondClient.NumberAsync().Result;
            string pathFullSecondStringResult = pathFullSecondClient.StringAsync().Result;

            var pathCoreFirstClient = new PathCoreAPI.FirstClient(coreClient);
            int pathCoreFirstNUmberResult = pathCoreFirstClient.NumberAsync().Result;
            string pathCoreFirstStringResult = pathCoreFirstClient.StringAsync().Result;
            var pathCoreSecondClient = new PathCoreAPI.SecondClient(coreClient);
            int pathCoreSecondNumberResult = pathCoreSecondClient.NumberAsync().Result;
            string pathCoreSecondStringResult = pathCoreSecondClient.StringAsync().Result;
            */
        }
    }
}
