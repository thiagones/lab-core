using System;
using lab.mq.Events.Handlers;

namespace lab.mq.Interfaces
{
    public interface IMessageQueueConnection : IDisposable
    {
        event MessageQueueReceivedMessageEventHandler ReceivedMessage;

        void Connect();

        /// <summary>
        /// Posta uma mensagem no broker
        /// </summary>
        /// <param name="destination">Destino da mensagem</param>
        /// <param name="correlationId">ID de correlação</param>
        /// <param name="command">Comando</param>
        /// <param name="message">Mensagem</param>
        /// <param name="repplyTo">Destino para qual será respondida (opcional)</param>
        void PostMessage(string destination, string correlationId, string command, string message, string repplyTo = "");

        /// <summary>
        /// Escuta uma determinada fila com ou sem filtro de seleção
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="selector"></param>
        void ListenToQueue(string destination, string selector);

        /// <summary>
        ///  Escuta determinado tópico com ou sem filtro de seleção
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="selector"></param>
        void ListenToTopic(string destination, string selector, Func<string, string, string, bool> receivedMessage);


    }
}