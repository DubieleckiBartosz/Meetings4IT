using Bogus;
using Meetings4IT.IntegrationTests.Constants;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Domain.Meetings;
using Panels.Domain.Meetings.ValueObjects;
using Panels.Infrastructure.Database.Domain.Seed;

namespace Meetings4IT.IntegrationTests.Generators.Panels.Meetings;

public static class MeetingGenerators
{
    public static Meeting GetMeeting()
    {
        var meetingFaker = new Faker<Meeting>(FakerSettings.EnglishLocalCode)
            .CustomInstantiator(f =>
              {
                  var city = f.Address.City();
                  var street = f.Address.StreetName();
                  var numberStreet = f.Address.BuildingNumber();
                  var isPublic = f.Random.Bool();
                  var maxInvitations = isPublic ? (int?)null : f.Random.Int(10, 20);
                  var hasPanelVisibility = f.Random.Bool();

                  var address = Address.Create(city, street, numberStreet);
                  var description = f.Lorem.Sentence();
                  var meetingCategory = f.PickRandom(SeedData.MeetingCategories());
                  var organizer = new UserInfo(GlobalUserData.Identifier, GlobalUserData.UserName);
                  var startDate = Clock.CurrentDate().AddDays(f.Random.Int(7, 14));
                  var dateRange = new DateRange(startDate, null);

                  return Meeting.Create(
                      organizer,
                      meetingCategory,
                      description,
                      address,
                      dateRange,
                      isPublic,
                      hasPanelVisibility,
                      maxInvitations);
              });

        Meeting meeting = meetingFaker.Generate();

        return meeting;
    }

    public static Meeting GetMeetingWthInvotations(int cnt = 1)
    {
        var faker = new Faker(FakerSettings.EnglishLocalCode);
        var meeting = GetMeeting();

        for (int i = 0; i < cnt; i++)
        {
            var recipientName = faker.Internet.UserName();
            var invitationExpirationDate = meeting.Date.StartDate.AddDays(-faker.Random.Int(2, 5));
            var email = faker.Internet.Email();

            meeting.CreateNewInvitation(email!, invitationExpirationDate, recipientName);
        }

        return meeting;
    }
}