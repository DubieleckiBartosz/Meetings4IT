using AutoFixture;
using Bogus;
using Meetings4IT.IntegrationTests.Constants;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Application.Models.Parameters.MeetingParams;
using Panels.Domain.Meetings;

namespace Meetings4IT.IntegrationTests.Modules.Panels.Requests;

public static class MeetingRequests
{
    public static DeclareNewMeetingParameters DeclareNewMeetingRequest(this Fixture fixture, int categoryIndex = 1)
    {
        var request = fixture.Build<DeclareNewMeetingParameters>()
            .With(_ => _.IndexCategory, categoryIndex)
            .With(_ => _.IsPublic, false)
            .With(_ => _.StartDate, Clock.CurrentDate().AddDays(7))
            .With(_ => _.NumberStreet, fixture.Create<uint>().ToString())
            .Without(_ => _.EndDate).Create();

        return request;
    }

    public static CreateMeetingInvitationParameters CreateMeetingInvitationRequest(Meeting meeting)
    {
        var invitationParametersFaker = new Faker<CreateMeetingInvitationParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id)
            .RuleFor(p => p.InvitationExpirationDate, f => meeting.Date.StartDate.AddDays(-1))
            .RuleFor(p => p.EmailInvitationRecipient, f => f.Internet.Email())
            .RuleFor(p => p.NameInvitationRecipient, f => f.Name.FullName());

        var invitationParameters = invitationParametersFaker.Generate();
        return invitationParameters;
    }

    public static CancelMeetingParameters CancelMeetingRequest(Meeting meeting)
    {
        var cancelMeetingParameters = new Faker<CancelMeetingParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id)
            .RuleFor(p => p.Reason, f => f.Lorem.Sentence());

        return cancelMeetingParameters.Generate();
    }

    public static AcceptMeetingInvitationParameters AcceptMeetingInvitationRequest(Meeting meeting, string code)
    {
        var cancelMeetingParameters = new Faker<AcceptMeetingInvitationParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id)
            .RuleFor(p => p.InvitationCode, f => code);

        return cancelMeetingParameters.Generate();
    }

    public static RejectMeetingInvitationParameters RejectMeetingInvitationParametersRequest(Meeting meeting, string code)
    {
        var cancelMeetingParameters = new Faker<RejectMeetingInvitationParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id)
            .RuleFor(p => p.InvitationCode, f => code);

        return cancelMeetingParameters.Generate();
    }

    public static AddInvitationRequestParameters AddInvitationRequestParametersRequest(Meeting meeting)
    {
        var addInvitationRequestParameters = new Faker<AddInvitationRequestParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id);

        return addInvitationRequestParameters.Generate();
    }

    public static RejectInvitationRequestParameters RejectInvitationRequestParametersRequest(Meeting meeting, int invitationRequestId)
    {
        var rejectInvitationRequestParameters = new Faker<RejectInvitationRequestParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id)
            .RuleFor(p => p.Reason, f => f.Lorem.Sentence())
            .RuleFor(p => p.InvitationRequestId, f => invitationRequestId);

        return rejectInvitationRequestParameters.Generate();
    }

    public static DeleteInvitationRequestParameters DeleteInvitationRequestParametersRequest(Meeting meeting)
    {
        var deleteInvitationRequestParameters = new Faker<DeleteInvitationRequestParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id);

        return deleteInvitationRequestParameters.Generate();
    }

    public static AddMeetingCommentParameters AddMeetingCommentParametersRequest(Meeting meeting)
    {
        var addMeetingCommentParameters = new Faker<AddMeetingCommentParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id)
            .RuleFor(p => p.Content, f => f.Lorem.Sentence());

        return addMeetingCommentParameters.Generate();
    }

    public static UpdateMeetingCommentParameters UpdateMeetingCommentParametersRequest(Meeting meeting, int commentId)
    {
        var updateMeetingCommentParameters = new Faker<UpdateMeetingCommentParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id)
            .RuleFor(p => p.CommentId, f => commentId)
            .RuleFor(p => p.Content, f => f.Lorem.Sentence());

        return updateMeetingCommentParameters.Generate();
    }

    public static DeleteMeetingCommentParameters DeleteMeetingCommentParametersRequest(Meeting meeting, int commentId)
    {
        var cancelMeetingParameters = new Faker<DeleteMeetingCommentParameters>(FakerSettings.EnglishLocalCode)
            .RuleFor(p => p.MeetingId, f => meeting.Id)
            .RuleFor(p => p.CommentId, f => commentId);

        return cancelMeetingParameters.Generate();
    }
}