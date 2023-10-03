using AutoFixture;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Application.Models.Parameters;

namespace Meetings4IT.IntegrationTests.Modules.Panels.Generators;

public static class MeetingRequestsGenerator
{
    public static DeclareNewMeetingParameters GetDeclareNewMeetingParameters(this Fixture fixture, int categoryIndex = 1)
    {
        var request = fixture.Build<DeclareNewMeetingParameters>()
            .With(_ => _.IndexCategory, categoryIndex)
            .With(_ => _.IsPublic, false)
            .With(_ => _.StartDate, Clock.CurrentDate().AddDays(7))
            .Without(_ => _.EndDate).Create();

        return request;
    }
}