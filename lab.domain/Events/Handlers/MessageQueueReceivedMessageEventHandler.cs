
using lab.domain.Events.Args;
using lab.domain.Interfaces.Messaging;

namespace lab.domain.Events.Handlers
{
        public delegate void MessageQueueReceivedMessageEventHandler(IMessageQueueConnection messageConnector, ReceivedMessageEventArgs e);
}