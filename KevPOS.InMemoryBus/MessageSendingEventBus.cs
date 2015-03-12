//using System;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using KevPOS.Infrastructure;
//using KevPOS.Messages;
//using log4net;

//namespace KevPOS.InMemoryBus
//{
//    public class MessageSendingEventBus : IEventPublisher
//    {
//        private static readonly ILog Logger = LogManager.GetLogger(typeof (CategoryNameChangedHandler));

//        public void Publish<T>(T @event) where T : AbstractEvent
//        {
//            using (var httpClient = new HttpClient())
//            {
//                //todo sort url
//                Task<HttpResponseMessage> response =
//                    httpClient.PostAsync(
//                        string.Format("http://www.kevposeventprocessorapi.dev/events/{0}", @event.GetType().Name),
//                        new StringContent(Serialiser.Serialise(@event), Encoding.UTF8, "application/json"));
//                try
//                {
//                    response.Result.EnsureSuccessStatusCode();
//                }
//                catch (Exception exception)
//                {
//                    Logger.ErrorFormat("Event failed to send, AggregateId {0}, exception {1}", @event.AggregateId,
//                        exception);
//                    throw;
//                }
//            }
//        }
//    }
//}

