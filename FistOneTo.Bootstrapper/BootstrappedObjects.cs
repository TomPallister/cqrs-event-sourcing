using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstOneTo.Authentication.Service;
using FirstOneTo.Handlers;
using FirstOneTo.Readmodel.Services;

namespace FistOneTo.Bootstrapper
{
    public class BootstrappedObjects
    {
        public BootstrappedObjects(CommandAndEventHandler commandAndEventHandler,
            IAuthenticationService authenticationService)
        {
            CommandAndEventHandler = commandAndEventHandler;
            AuthenticationService = authenticationService;
        }
        public CommandAndEventHandler CommandAndEventHandler { get; private set; }
        public IAuthenticationService AuthenticationService { get; private set; }

    }
}
