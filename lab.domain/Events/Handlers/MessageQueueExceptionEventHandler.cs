
using lab.domain.Events.Args;

namespace lab.domain.Events.Handlers
{
    public delegate void MessageQueueExceptionEventHandler(object sender, MessageQueueExeptionEventArgs e);
}