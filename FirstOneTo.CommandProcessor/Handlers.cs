using System.Collections.Generic;
using FirstOneTo.Commands;
using FirstOneTo.Domain.Service.Interface;
using KevPOS.Messages;

namespace FirstOneTo.CommandProcessor
{
    internal static class Handlers
    {
        public static IList<AbstractEvent> CreateExample(IExampleService exampleService,
            CreateExample command)
        {
            return exampleService.CreateExample(command);
        }

        public static IList<AbstractEvent> ChangeExample(IExampleService exampleService, ChangeExample command)
        {
            return exampleService.ChangeExample(command);
        }
    }
}