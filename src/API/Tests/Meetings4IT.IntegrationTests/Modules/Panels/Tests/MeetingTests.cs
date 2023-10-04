using Meetings4IT.IntegrationTests.Modules.Panels.Generators;
using Meetings4IT.IntegrationTests.Setup;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Domain.Meetings;
using Panels.Infrastructure.Database;

namespace Meetings4IT.IntegrationTests.Modules.Panels.Tests;

public class MeetingTests : ControllerBaseTests
{
    public MeetingTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_Create_New_Meeting()
    {
        //Arrange
        var request = Fixture.GetDeclareNewMeetingRequest();

        //Act
        var response = await ClientCall(request, HttpMethod.Post, Urls.DeclareNewMeetingPath);
        var responseData = await ReadFromResponse<Response<int>>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
        Assert.True(responseData!.Data > 0);
    }

    [Fact]
    public async Task Should_Create_New_Meeting_Invitation()
    {
        //Arrange
        var meeting = Fixture.GetMeeting();

        InitData<PanelContext, Meeting>(meeting);

        var request = Fixture.GetCreateMeetingInvitationRequest(meeting);

        //Act
        var response = await ClientCall(request, HttpMethod.Post, Urls.CreateNewMeetingInvitationPath);
        var responseData = await ReadFromResponse<Response<int>>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
        Assert.True(responseData!.Data > 0);
    }
}