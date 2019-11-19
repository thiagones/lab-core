using lab.domain.Exceptions;

namespace lab.mq.Events.Args
{
    public sealed class MessageQueueExeptionEventArgs
    {
        public string CorrelationId { get; set; }

        public LabException exception { get; set; }
    }




}