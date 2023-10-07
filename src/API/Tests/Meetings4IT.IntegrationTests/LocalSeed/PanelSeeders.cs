using Dapper;
using Meetings4IT.IntegrationTests.Generators.Panels.Meetings;
using Microsoft.EntityFrameworkCore;
using Panels.Domain.Meetings;
using Panels.Infrastructure.Database;
using System.Data.SqlClient;

namespace Meetings4IT.IntegrationTests.LocalSeed;

public class PanelSeeders
{
    private const string _localConnectionString = "Server=localhost,1440;Database=Meetings4IT;User id=sa;Password=sql123456(!)";
    private const bool _meetingsSeedActive = false;

    //This method is intended for lacal tests only
    [Fact]
    public async Task MeetingsSeed()
    {
        if (!_meetingsSeedActive)
        {
            return;
        }

        using (var connection = new SqlConnection(_localConnectionString))
        {
            await connection.OpenAsync();

            var devName = "user_name";
            var devEmail = "user.meetings@dev.com";

            var resultUserIdentifier = (await connection.QueryAsync<string>("SELECT Id FROM [Meetings4IT].[identities].[AspNetUsers] WHERE Email = @email", new { Email = devEmail })).Single();

            var optionsBuilder = new DbContextOptionsBuilder<PanelContext>().UseSqlServer(_localConnectionString);

            using (var panelContext = new PanelContext(optionsBuilder.Options))
            {
                var meetings = new List<Meeting>();
                var emptyMeeting = MeetingGenerators.GetActiveMeeting(resultUserIdentifier, devName);
                var meetingWithComment = MeetingGenerators.GetMeetingWthComment(MeetingGenerators.GetActiveMeeting(resultUserIdentifier, devName));
                var meetingWithInvotations = MeetingGenerators.GetMeetingWthInvotations(3, organizerId: resultUserIdentifier, organizerName: devName);
                var meetingWithRequests = MeetingGenerators.GetMeetingWthInvotationRequests(MeetingGenerators.GetActiveMeeting(resultUserIdentifier, devName), cnt: 3);

                var fullMeeting = MeetingGenerators.GetMeetingWthInvotations(10, organizerId: resultUserIdentifier, organizerName: devName);
                fullMeeting = MeetingGenerators.GetMeetingWthInvotationRequests(fullMeeting);

                meetings.Add(emptyMeeting);
                meetings.Add(meetingWithComment);
                meetings.Add(meetingWithInvotations);
                meetings.Add(meetingWithRequests);
                meetings.Add(fullMeeting);

                await panelContext.Meetings.AddRangeAsync(meetings);
                await panelContext.SaveChangesAsync();
            }
        }
    }
}