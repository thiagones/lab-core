using lab.domain.Exceptions;

namespace lab.domain.Events.Args
{
    public sealed class MessageQueueExeptionEventArgs
    {
        public string CorrelationId { get; set; }

        public LabException exception { get; set; }
    }




}