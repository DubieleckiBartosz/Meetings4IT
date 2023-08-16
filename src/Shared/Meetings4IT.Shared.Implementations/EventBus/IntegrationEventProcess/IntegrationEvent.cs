using System.Text.Json.Serialization;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

public record IntegrationEvent
{
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    [Newtonsoft.Json.JsonConstructor]
    public IntegrationEvent(Guid id, DateTime createDate)
    {
        Id = id;
        CreationDate = createDate;
    }

    [JsonInclude]
    public Guid Id { get; init; }

    [JsonInclude]
    public DateTime CreationDate { get; init; }
}