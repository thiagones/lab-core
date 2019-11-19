namespace lab.mq.Events.Args
{
    public sealed class ReceivedMessageEventArgs
    {
        public string CorrelationId { get; set; }

        public string ReplyTo { get; set; }

        public string Command { get; set; }

        public string Message { get; set; }

    }

    

    
}