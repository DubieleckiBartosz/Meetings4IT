using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters;

public class DeleteUserOpinionParameters
{
    public int UserId { get; init; }
    public int OpinionId { get; init; }

    public DeleteUserOpinionParameters()
    {
    }

    [JsonConstructor]
    public DeleteUserOpinionParameters(int userId, int opinionId) => (UserId, OpinionId) = (userId, opinionId);
}