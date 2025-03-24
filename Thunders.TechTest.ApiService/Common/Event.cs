using MediatR;
using Rebus.Messages;

namespace Thunders.TechTest.ApiService.Common;

public abstract class Event : INotification
{
    public DateTime Timestamp { get; private set; }

    protected Event()
    {
        Timestamp = DateTime.Now;
    }


}