using lab.mq.Interfaces;

namespace lab.mq.Messaging
{
    public class RabbitMQConnection : IMessageQueueConnection
    {
        public RabbitMQConnection()
        {
        }
        
        public void Connect()
        {

        }

        public void PostMessage(string destination, string correlationId, string command, string message, int delay, string repplyTo = "")
        {

        }

        public void ListenToQueue(string destination, string selector)
        {

        }

        public void ListenToTopic(string destination, string selector)
        {
            
        }

    }
}