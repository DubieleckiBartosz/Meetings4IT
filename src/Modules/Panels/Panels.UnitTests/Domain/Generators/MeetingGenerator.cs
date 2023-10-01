using AutoFixture;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Domain.Meetings;
using Panels.Domain.Meetings.Categories;
using Panels.Domain.Meetings.ValueObjects;

namespace Panels.UnitTests.Domain.Generators;

public static class MeetingGenerator
{
    public static Meeting GetMeeting(this Fixture fixture, bool isPublic = true, bool hasPanelVisibility = true)
    {
        var organizerId = fixture.Create<string>();
        var organizerName = fixture.Create<string>();
        var city = fixture.Create<string>();
        var street = fixture.Create<string>();
        var numberStreet = fixture.Create<string>();
        var indexCategory = fixture.Create<int>();
        var category = fixture.Create<string>();
        var maxInvitations = isPublic ? null : fixture.Create<int?>();
        var startDate = Clock.CurrentDate().AddDays(5);
        var endDate = startDate.AddDays(1);

        Address address = Address.Create(city, street, numberStreet);
        Description description = fixture.Create<string>();
        MeetingCategory meetingCategory = new MeetingCategory(indexCategory, category);
        UserInfo organizer = new UserInfo(organizerId, organizerName);
        DateRange dateRange = new DateRange(startDate, endDate);

        var result = Meeting.Create(
            organizer,
            meetingCategory,
            description,
            address,
            dateRange,
            isPublic,
            hasPanelVisibility,
            maxInvitations);

        return result;
    }
}