﻿using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using System.Text.Json;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog;

public class IntegrationEventLog
{
    public IntegrationEventLog(IntegrationEvent @event)
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
    }

    public string EventTypeShortName => EventTypeName!.Split('.').Last();
    public Guid EventId { get; }
    public string EventTypeName { get; }
    public IntegrationEvent? IntegrationEvent { get; private set; }
    public EventState State { get; private set; }
    public int TimesSent { get; private set; }
    public DateTime CreationTime { get; }
    public string Content { get; }

    public IntegrationEventLog DeserializeJsonContent(Type type)
    {
        IntegrationEvent =
            JsonSerializer.Deserialize(Content, type, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) as
                IntegrationEvent;

        return this;
    }
}