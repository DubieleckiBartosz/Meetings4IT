using Moq;
using System.Reflection;

namespace Meetings4IT.IntegrationTests.Setup;

public class MockAbstractions
{
    public (Type underlyingType, object obj)[] GetMocks()
    {
        return this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(_ =>
        {
            var underlyingType = _.PropertyType.GetGenericArguments()[0];
            var value = _.GetValue(this) as Mock;

            return (underlyingType, value?.Object!);
        }).ToArray();
    }

    public static void RegisterMockServices(ref IServiceCollection services, (Type underlyingType, object obj)[] mocks)
    {
        foreach (var (interfaceType, serviceMock) in mocks)
        {
            var serviceToRemove = services.FirstOrDefault(x => x.ServiceType == interfaceType);
            services.Remove(serviceToRemove!);
            services.AddSingleton(interfaceType, serviceMock);
        }
    }
}