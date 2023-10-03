using Meetings4IT.IntegrationTests.Modules.Panels.Generators;
using Meetings4IT.IntegrationTests.Setup;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Domain.Meetings.Categories;
using Panels.Infrastructure.Database;

namespace Meetings4IT.IntegrationTests.Modules.Panels.Tests;

public class MeetingTests : ControllerBaseTests
{
    public MeetingTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
        var list = new List<MeetingCategory>()
    {
            new MeetingCategory(1, "Party"),
            new MeetingCategory(2, "Social"),
            new MeetingCategory(3, "Business"),
            new MeetingCategory(4, "SomeCoffee"),
            new MeetingCategory(5, "Mentoring"),
            new MeetingCategory(6, "Unknown")
    };

        InitData<PanelContext, MeetingCategory>(list);
    }

    [Fact]
    public async Task Should_Create_New_Meeting()
    {
        //Arrange
        var request = Fixture.GetDeclareNewMeetingParameters();

        //Act
        var response = await ClientCall(request, HttpMethod.Post, Urls.DeclareNewMeetingPath);
        var responseData = await ReadFromResponse<Response<int>>(response);

        //Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.True(responseData!.Success);
    }
}