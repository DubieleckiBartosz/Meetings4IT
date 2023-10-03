using AutoFixture;
using Meetings4IT.IntegrationTests.Constants;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Domain.Meetings;
using Panels.Domain.Meetings.Categories;
using Panels.Domain.Meetings.ValueObjects;

namespace Meetings4IT.IntegrationTests.Modules.Panels.Generators;

public static class MeetingGenerators
{
    public static Meeting GetMeeting(this Fixture fixture)
    {
        var rnd = new Random();
        var city = fixture.Create<string>();
        var street = fixture.Create<string>();
        var numberStreet = fixture.Create<string>();
        var categoryFakeValue = fixture.Create<string>();
        var categoryFakeIndex = fixture.Create<int>();
        var isPublic = fixture.Create<bool>();
        var maxInvitations = isPublic ? (int?)null : rnd.Next(10, 20);
        var hasPanelVisibility = fixture.Create<bool>();

        Address address = Address.Create(city, street, numberStreet);
        Description description = fixture.Create<string>();
        MeetingCategory meetingCategory = new MeetingCategory(categoryFakeIndex, categoryFakeValue);
        UserInfo organizer = new UserInfo(GlobalUserData.Identifier, GlobalUserData.UserName);
        DateRange dateRange = new DateRange(Clock.CurrentDate().AddDays(rnd.Next(7, 14)), null);

        var meeting = Meeting.Create(
            organizer,
            meetingCategory,
            description,
            address,
            dateRange,
            isPublic,
            hasPanelVisibility,
            maxInvitations);

        return meeting;
    }
}