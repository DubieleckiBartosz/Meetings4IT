using Meetings4IT.Shared.Abstractions.Events;
using Microsoft.AspNetCore.Identity;

namespace Identities.Core.Models.Entities;

public class ApplicationUser : IdentityUser
{
    private readonly Queue<IDomainEvent> _events = new();

    public List<IDomainEvent> Events => _events.ToList();

    private void AddEvent(IDomainEvent @event) => _events.Enqueue(@event);

    public IDomainEvent? DequeueEvent()
    {
        if (_events.TryDequeue(out var dequeuedEvent))
        {
            return dequeuedEvent;
        }

        return null; 
    } 

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}