namespace Identities.Core.Options;

public class OpenIdDictOptions
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string Scope { get; set; } = default!;
}