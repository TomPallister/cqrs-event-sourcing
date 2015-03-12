using System;
using System.Collections.Generic;
using FirstOneTo.CommandProcessor;
using FirstOneTo.Commands;
using FirstOneTo.CommandValidation;
using FirstOneTo.EventsPublisher;
using KevPOS.IdGenerator;
using KevPOS.Messages;
using KevPOS.ValueObjects;
using Newtonsoft.Json;

namespace FirstOneTo.Handlers
{
    public class CommandAndEventHandler
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IEventsPublisher _eventsPublisher;
        private readonly Dictionary<string, Func<AbstractCommand, ValidationResult>> _handlers;
        private readonly IIdGenerator _idGenerator;
        private readonly IValidator _validator;

        public CommandAndEventHandler(IIdGenerator idGenerator, ICommandProcessor commandProcessor,
            IEventsPublisher eventsPublisher, IValidator validator)
        {
            _eventsPublisher = eventsPublisher;
            _commandProcessor = commandProcessor;
            _idGenerator = idGenerator;
            _validator = validator;
            _handlers = new Dictionary<string, Func<AbstractCommand, ValidationResult>>
            {
                {"CreateExample", CreateExample},
                {"CreateUser", CreateUser},
            };
        }

        public ResponseContent Process(string commandName, AbstractCommand command)
        {
            Func<AbstractCommand, ValidationResult> handler;
            _handlers.TryGetValue(commandName, out handler);
            if (handler == null) return null;
            ValidationResult validationResult = handler(command);
            return new ResponseContent
            {
                AggregateId = validationResult.AggregateId,
                ErrorMessages = validationResult.ErrorMessages,
                Valid = validationResult.Valid
            };
        }

        private void Process(AbstractCommand command)
        {
            IList<AbstractEvent> evts = _commandProcessor.Accept(command);
            _eventsPublisher.Publish(evts);
        }

        private ValidationResult CreateExample(AbstractCommand createExample)
        {
            createExample.AggregateId = _idGenerator.NextId();
            ValidationResult validationResult = _validator.ValidateCreateExample((CreateExample) createExample);
            if (validationResult.Valid)
            {
                Process(createExample);
                validationResult.AggregateId = createExample.AggregateId;
            }
            return validationResult;
        }

        private ValidationResult CreateUser(AbstractCommand createUser)
        {
            createUser.AggregateId = _idGenerator.NextId();
            ValidationResult validationResult = _validator.ValidateCreateUser((CreateUser) createUser);
            if (validationResult.Valid)
            {
                Process(createUser);
                validationResult.AggregateId = createUser.AggregateId;
            }
            return validationResult;
        }

        private Dictionary<string, object> DeserialiseJson(string json)
        {
            var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return obj;
        }

        private string SerialiseObject<T>(T command) where T : AbstractCommand
        {
            return JsonConvert.SerializeObject(command);
        }
    }
}