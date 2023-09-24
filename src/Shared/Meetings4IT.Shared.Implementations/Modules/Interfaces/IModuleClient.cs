namespace Meetings4IT.Shared.Implementations.Modules.Interfaces;

public interface IModuleClient
{
    Task<TResult?> SendAsync<TResult>(string path, object request, CancellationToken cancellationToken = default) where TResult : class;
}