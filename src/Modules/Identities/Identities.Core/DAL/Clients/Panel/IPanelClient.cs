using Meetings4IT.Shared.Implementations.Wrappers;
using static Identities.Core.DAL.Clients.Panel.PanelClient;

namespace Identities.Core.DAL.Clients.Panel;

internal interface IPanelClient
{
    Task<ResponseClient<string>> CreateNewPanelUserAsync(CreateNewUserRequest request);
}