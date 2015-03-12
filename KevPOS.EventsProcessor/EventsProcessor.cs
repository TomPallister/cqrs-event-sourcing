using System.Collections.Generic;
using FirstOneTo.Authentication.Service;
using FirstOneTo.EventHandlers;
using FirstOneTo.Events;
using FirstOneTo.Readmodel.Services.Interface;
using KevPOS.Messages;
using Tasks.Command.Services.Interface;

namespace KevPOS.EventsProcessor
{
    public class EventsProcessor : IEventsProcessor
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICommandExampleService _commandExampleService;
        private readonly EventsHandler _eventsHandler;
        private readonly IQueryExampleService _queryExampleService;

        public EventsProcessor(IAuthenticationService authenticationService,
            ICommandExampleService commandExampleService,
            IQueryExampleService queryExampleService)

        {
            _authenticationService = authenticationService;
            _queryExampleService = queryExampleService;
            _commandExampleService = commandExampleService;
            _eventsHandler = new EventsHandler();
            Bootstrap();
        }

        public void Accept<TEvent>(TEvent evt) where TEvent : AbstractEvent
        {
            _eventsHandler.Handle(evt);
        }

        public void Accept<TEvent>(List<TEvent> evts) where TEvent : AbstractEvent
        {
            foreach (TEvent evt in evts)
            {
                _eventsHandler.Handle(evt);
            }
        }

        public void Bootstrap()
        {
            _eventsHandler.Add<ExampleCreated>(
                evt =>
                    ExampleHandlers.ExampleCreated(evt, _commandExampleService));
            _eventsHandler.Add<ExampledChanged>(
                evt =>
                    ExampleHandlers.ExampledChanged(evt, _commandExampleService));
        }
    }
}