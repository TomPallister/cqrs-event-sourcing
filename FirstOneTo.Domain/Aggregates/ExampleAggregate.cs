using System.Collections.Generic;
using FirstOneTo.Commands;
using FirstOneTo.Events;
using KevPOS.Domain.Aggregates;
using KevPOS.Messages;
using KevPOS.ValueObjects;

namespace FirstOneTo.Domain.Aggregates
{
    public class ExampleAggregate : AggregateRoot
    {
        private Description _description;

        public ExampleAggregate(long id)
            : base(id)
        {
            HandlesEvent<ExampleCreated>(OnChallengeCreated);
            HandlesEvent<ExampledChanged>(OnChallengeChanged);
        }

        public ExampleAggregate(long id, IEnumerable<AbstractEvent> events)
            : this(id)
        {
            LoadFrom(events);
        }

        public ExampleAggregate(CreateExample createExampleCommand)
            : this(createExampleCommand.AggregateId)
        {
            Update(new ExampleCreated
            {
                AggregateId = createExampleCommand.AggregateId,
                Description = createExampleCommand.Description
            });
        }

        public void ChangeExampleDescription(ChangeExample changeExampleCommand)
        {
            Update(new ExampledChanged
            {
                Description = changeExampleCommand.Description
            });
        }

        private void OnChallengeCreated(ExampleCreated exampleAggregateCreated)
        {
            _description = new Description(exampleAggregateCreated.Description);
        }

        private void OnChallengeChanged(ExampledChanged exampleAggregateChanged)
        {
            _description = new Description(exampleAggregateChanged.Description);
        }
    }
}