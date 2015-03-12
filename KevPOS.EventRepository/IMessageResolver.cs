using System;
using System.Linq;
using System.Reflection;

namespace KevPOS.EventRepository
{
    public interface IMessageResolver
    {
        Type TypeForName(string name);
    }

    public class MessageResolver : IMessageResolver
    {
        private readonly Assembly _messageAssembly;

        public MessageResolver(Assembly messageAssembly)
        {
            _messageAssembly = messageAssembly;
        }

        public Type TypeForName(string name)
        {
            //Log4NetLogger.LogEntry(GetType(), "TypeForName", string.Format("name = {0}", name), LoggerLevel.Debug);

            Type[] types = _messageAssembly.GetTypes();
            //Log4NetLogger.LogEntry(GetType(), "TypeForName", string.Format("types count = {0}", types.Length), LoggerLevel.Debug);
            foreach (Type type in types)
            {
                //Log4NetLogger.LogEntry(GetType(), "TypeForName", string.Format("types = {0}", type.Name), LoggerLevel.Debug);
            }
            return types.First(type => type.Name == name);
        }
    }
}