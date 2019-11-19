
using lab.mq.Events.Args;
using lab.mq.Interfaces;

namespace lab.mq.Events.Handlers
{
        public delegate void MessageQueueReceivedMessageEventHandler(IMessageQueueConnection messageConnector, ReceivedMessageEventArgs e);
}