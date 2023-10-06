using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.UserParams;

public class CompleteUserDetailsParameters
{
    public List<string>? Technologies { get; init; }
    public IFormFile? Image { get; init; }

    public CompleteUserDetailsParameters()
    { }

    [JsonConstructor]
    public CompleteUserDetailsParameters(List<string>? technologies, IFormFile? image)
    {
        Technologies = technologies;
        Image = image;
    }
}