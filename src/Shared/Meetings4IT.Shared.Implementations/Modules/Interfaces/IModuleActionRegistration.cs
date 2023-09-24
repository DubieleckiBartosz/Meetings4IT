namespace Meetings4IT.Shared.Implementations.Modules.Interfaces;

public interface IModuleActionRegistration
{
    void AddRequestAction(string path, Type requestType, Type responseType,
        Func<object, CancellationToken, Task<object>> action);

    ModuleRequestRegistration? GetRequestRegistration(string path);
}