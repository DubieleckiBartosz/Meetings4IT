using System.Text.Json;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog;

public class IntegrationEventLog
{
    public IntegrationEventLog(IntegrationEvent @event, Guid transactionId)
    {
        EventId = @event.Id;
        CreationTime = @event.CreationDate;
        EventTypeName = @event.GetType().FullName!;
        Content = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true
        });
        State = EventState.NotPublished;
        TimesSent = 0;
        TransactionId = transactionId.ToString();
    }

    public string EventTypeShortName => EventTypeName!.Split('.').Last();
    public Guid EventId { get; }
    public string EventTypeName { get; }
    public IntegrationEvent? IntegrationEvent { get; private set; }
    public EventState State { get; private set; }
    public int TimesSent { get; private set; }
    public DateTime CreationTime { get; }
    public string Content { get; }
    public string TransactionId { get; }

    public IntegrationEventLog DeserializeJsonContent(Type type)
    {
        IntegrationEvent =
            JsonSerializer.Deserialize(Content, type, new JsonSerializerOptions {PropertyNameCaseInsensitive = true}) as
                IntegrationEvent;

        return this;
    }
}