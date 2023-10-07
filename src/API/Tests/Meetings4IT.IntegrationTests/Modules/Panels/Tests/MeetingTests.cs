using Meetings4IT.IntegrationTests.Constants;
using Meetings4IT.IntegrationTests.Generators.Panels.Meetings;
using Meetings4IT.IntegrationTests.Modules.Panels.Requests;
using Meetings4IT.IntegrationTests.Setup;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.EntityFrameworkCore;
using Panels.Domain.Meetings;
using Panels.Domain.Meetings.Statuses;
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
        var request = Fixture.DeclareNewMeetingRequest();

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
        var meeting = MeetingGenerators.GetActiveMeeting();

        InitData<PanelContext, Meeting>(meeting);

        var request = MeetingRequests.CreateMeetingInvitationRequest(meeting);

        //Act
        var response = await ClientCall(request, HttpMethod.Post, Urls.CreateNewMeetingInvitationPath);
        var responseData = await ReadFromResponse<Response<int>>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
        Assert.True(responseData!.Data > 0);
    }

    [Fact]
    public async Task Should_Cancel_Meeting()
    {
        //Arrange
        var meeting = MeetingGenerators.GetMeetingWthInvotations();

        InitData<PanelContext, Meeting>(meeting);

        var request = MeetingRequests.CancelMeetingRequest(meeting);

        //Act
        var response = await ClientCall(request, HttpMethod.Put, Urls.CancelMeetingPath);
        var responseData = await ReadFromResponse<Response>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
    }

    [Fact]
    public async Task Should_Accept_Meeting_Invotation()
    {
        //Arrange
        var meeting = MeetingGenerators.GetMeetingWthInvotations();

        InitData<PanelContext, Meeting>(meeting);

        var code = meeting.Invitations[0].Code.Value;
        var request = MeetingRequests.AcceptMeetingInvitationRequest(meeting, code);

        //Act
        var response = await ClientCall(request, HttpMethod.Put, Urls.AcceptInvitationPath);
        var responseData = await ReadFromResponse<Response>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);

        await AssertWithContext<PanelContext>(async _ =>
        {
            var result = await _.Meetings
            .Include(RepoPathQueries.Invitations)
            .FirstOrDefaultAsync(_ => _.Id == meeting.Id);

            var invotation = result!.Invitations.First(i => i.Code == code);
            Assert.NotNull(invotation);
            Assert.True(invotation.Status == InvitationStatus.Accepted);
        });
    }

    [Fact]
    public async Task Should_Reject_Meeting_Invotation()
    {
        //Arrange
        var meeting = MeetingGenerators.GetMeetingWthInvotations();

        InitData<PanelContext, Meeting>(meeting);

        var code = meeting.Invitations[0].Code.Value;
        var request = MeetingRequests.RejectMeetingInvitationParametersRequest(meeting, code);

        //Act
        var response = await ClientCall(request, HttpMethod.Put, Urls.RejectInvitationPath);
        var responseData = await ReadFromResponse<Response>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);

        await AssertWithContext<PanelContext>(async _ =>
        {
            var result = await _.Meetings
            .Include(RepoPathQueries.Invitations)
            .FirstOrDefaultAsync(_ => _.Id == meeting.Id);

            var invotation = result!.Invitations.First(i => i.Code == code);
            Assert.NotNull(invotation);
            Assert.True(invotation.Status == InvitationStatus.Rejected);
        });
    }

    [Fact]
    public async Task Should_Add_New_Invotation_Request()
    {
        //Arrange
        var meeting = MeetingGenerators.GetActiveMeeting();

        InitData<PanelContext, Meeting>(meeting);

        var request = MeetingRequests.AddInvitationRequestParametersRequest(meeting);

        //Act
        var response = await ClientCall(request, HttpMethod.Post, Urls.AddInvitationRequestPath);
        var responseData = await ReadFromResponse<Response>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
    }

    [Fact]
    public async Task Should_Reject_Invotation_Request()
    {
        //Arrange
        var meeting = MeetingGenerators.GetMeetingWthInvotationRequests();

        InitData<PanelContext, Meeting>(meeting);

        var request = MeetingRequests.RejectInvitationRequestParametersRequest(meeting, meeting.InvitationRequests[0].Id);

        //Act
        var response = await ClientCall(request, HttpMethod.Put, Urls.RejectInvitationRequestPath);
        var responseData = await ReadFromResponse<Response>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
    }

    [Fact]
    public async Task Should_Delete_Invotation_Request()
    {
        //Arrange
        var meeting = MeetingGenerators.GetMeetingWthInvotationRequest();

        InitData<PanelContext, Meeting>(meeting);

        var request = MeetingRequests.DeleteInvitationRequestParametersRequest(meeting);

        //Act
        var response = await ClientCall(request, HttpMethod.Delete, Urls.DeleteInvitationRequestPath);
        var responseData = await ReadFromResponse<Response>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
    }

    [Fact]
    public async Task Should_Add_Meeting_Comment()
    {
        //Arrange
        var meeting = MeetingGenerators.GetActiveMeeting();

        InitData<PanelContext, Meeting>(meeting);

        var request = MeetingRequests.AddMeetingCommentParametersRequest(meeting);

        //Act
        var response = await ClientCall(request, HttpMethod.Post, Urls.AddMeetingCommentPath);
        var responseData = await ReadFromResponse<Response>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
    }

    [Fact]
    public async Task Should_Update_Meeting_Comment()
    {
        //Arrange
        var meeting = MeetingGenerators.GetMeetingWthComment();

        InitData<PanelContext, Meeting>(meeting);

        var request = MeetingRequests.UpdateMeetingCommentParametersRequest(meeting, meeting.Comments[0].Id);

        //Act
        var response = await ClientCall(request, HttpMethod.Put, Urls.UpdateMeetingCommentPath);
        var responseData = await ReadFromResponse<Response>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
    }

    [Fact]
    public async Task Should_Delete_Meeting_Comment()
    {
        //Arrange
        var meeting = MeetingGenerators.GetMeetingWthComment();

        InitData<PanelContext, Meeting>(meeting);

        var request = MeetingRequests.DeleteMeetingCommentParametersRequest(meeting, meeting.Comments[0].Id);

        //Act
        var response = await ClientCall(request, HttpMethod.Delete, Urls.DeleteMeetingCommentPath);
        var responseData = await ReadFromResponse<Response>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
    }
}