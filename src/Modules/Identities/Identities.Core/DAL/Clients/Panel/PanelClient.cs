using Meetings4IT.Shared.Implementations.Modules.Interfaces;
using Meetings4IT.Shared.Implementations.Wrappers;

namespace Identities.Core.DAL.Clients.Panel;

internal class PanelClient : IPanelClient
{
    private readonly IModuleClient _client;

    internal record CreateNewUserRequest(string Email, string Name, string UserId, string City);

    public PanelClient(IModuleClient client)
    {
        _client = client;
    }

    public async Task<ResponseClient<string>> CreateNewPanelUserAsync(CreateNewUserRequest request)
    {
        var result = await _client.SendAsync<ResponseClient<string>>("user/create", request);
        return result ?? throw new InvalidOperationException("Response is null when calling the 'user/create' module client");
    }
}