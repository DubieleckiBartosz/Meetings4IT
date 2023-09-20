using AutoFixture;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.UnitTests.Domain.Generators;

namespace Panels.UnitTests.Domain.Meetings;

public class MeetingTests : DomainBaseTests
{
    [Fact]
    public void Should_Create_New_Invitation_With_Code()
    {
        var meeting = Fixture.GetMeeting();

        Date expiration = Clock.CurrentDate().AddDays(1);
        Email recipient = "test@test.com"!;

        var invitation = meeting.CreateNewInvitation(recipient, expiration);

        Assert.NotNull(invitation);
        Assert.NotNull(invitation.Code.Value);
    }
}