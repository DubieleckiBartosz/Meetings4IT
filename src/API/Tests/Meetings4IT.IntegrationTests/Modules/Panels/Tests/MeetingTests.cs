using Meetings4IT.IntegrationTests.Modules.Panels.Generators;
using Meetings4IT.IntegrationTests.Setup;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Domain.Meetings.Categories;
using Panels.Infrastructure.Database;
using Panels.Infrastructure.Database.Domain.Seed;

namespace Meetings4IT.IntegrationTests.Modules.Panels.Tests;

public class MeetingTests : ControllerBaseTests
{
    public MeetingTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
        InitData<PanelContext, MeetingCategory>(SeedData.MeetingCategories());
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