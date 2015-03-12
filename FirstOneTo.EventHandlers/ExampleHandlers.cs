using System;
using FirstOneTo.Events;
using FirstOneTo.Readmodel.Models;
using Tasks.Command.Services.Interface;

namespace FirstOneTo.EventHandlers
{
    public static class ExampleHandlers
    {
        public static void ExampleCreated(ExampleCreated evt, ICommandExampleService commandExampleService)
        {
            commandExampleService.Store(new ExampleModel(evt.AggregateId, evt.Description));
        }

        public static void ExampledChanged(ExampledChanged evt, ICommandExampleService commandExampleService)
        {
            throw new NotImplementedException();
        }
    }
}