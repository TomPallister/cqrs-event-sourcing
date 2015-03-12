namespace FirstOneTo.Readmodel.Models
{
    public class ExampleModel
    {
        public ExampleModel()
        {
        }

        public ExampleModel(long aggregateId, string description)
        {
            AggregateId = aggregateId;
            Description = description;
        }

        public long AggregateId { get; set; }
        public string Description { get; set; }
    }
}