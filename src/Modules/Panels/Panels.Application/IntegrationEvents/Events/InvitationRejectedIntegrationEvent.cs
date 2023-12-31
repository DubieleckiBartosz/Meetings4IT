﻿using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Panels.Application.IntegrationEvents.Events;

public record InvitationRejectedIntegrationEvent(string MeetingLink, string RecipientId, string RejectedBy) : IntegrationEvent;