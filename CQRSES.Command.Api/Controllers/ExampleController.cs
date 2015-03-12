using System.Web.Http;
using FirstOneTo.Commands;
using FirstOneTo.CommandSender;

namespace Tasks.Command.Api.Controllers
{
    public class ExampleController : ApiController
    {
        private readonly CommandSender _commandSender;

        public ExampleController(CommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        // POST api/example
        public void Post([FromBody] CreateExample createExample)
        {
            dynamic responseContent = _commandSender.Send("CreateExample", createExample);
        }
    }
}