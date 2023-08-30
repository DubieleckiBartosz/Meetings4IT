using Meetings4IT.Shared.Abstractions.Events; 

namespace Identities.Core.Models.Entities.DomainEvents;

public record UserRegistered(string Email, string TokenConfirmation) : IDomainEvent;