using System.Web.Http;
using FirstOneTo.Readmodel.Services.Interface;
using Newtonsoft.Json;

namespace Tasks.Query.Api.Controllers
{
    public class ExampleController : ApiController
    {
        private readonly IQueryExampleService _readModelExampleService;

        public ExampleController(IQueryExampleService readModelExampleService)
        {
            _readModelExampleService = readModelExampleService;
        }

        // GET api/example/5
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(_readModelExampleService.GetExample(id));
        }
    }
}