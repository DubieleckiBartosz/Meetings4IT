using AutoFixture;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Application.Models.Parameters;
using Panels.Domain.Meetings;

namespace Meetings4IT.IntegrationTests.Modules.Panels.Generators;

public static class MeetingRequestsGenerator
{
    public static DeclareNewMeetingParameters GetDeclareNewMeetingRequest(this Fixture fixture, int categoryIndex = 1)
    {
        var request = fixture.Build<DeclareNewMeetingParameters>()
            .With(_ => _.IndexCategory, categoryIndex)
            .With(_ => _.IsPublic, false)
            .With(_ => _.StartDate, Clock.CurrentDate().AddDays(7))
            .With(_ => _.NumberStreet, fixture.Create<uint>().ToString())
            .Without(_ => _.EndDate).Create();

        return request;
    }

    public static CreateMeetingInvitationParameters GetCreateMeetingInvitationRequest(this Fixture fixture, Meeting meeting)
    {
        var request = fixture.Build<CreateMeetingInvitationParameters>()
            .With(_ => _.MeetingId, meeting.Id)
            .With(_ => _.InvitationExpirationDate, meeting.Date.StartDate.AddDays(-1))
            .With(_ => _.EmailInvitationRecipient, "Test@test.com").Create();

        return request;
    }
}