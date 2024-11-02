using Splat;
using TSC.Splat.Extensions.Exceptions;

namespace TSC.Splat.Extensions;

public static class SplatExtensions {
    public static T GetRequiredService<T>(this IReadonlyDependencyResolver resolver) {
        var service = resolver.GetService<T>();
    
        if (service is null) {
            throw new ServiceNotFoundException();
        }
    
        return service;
    }
    
    public static T GetRequiredService<T>(this IReadonlyDependencyResolver resolver, T? service) {
        service = service ?? resolver.GetService<T>();

        if (service is null) {
            throw new ServiceNotFoundException();
        }
        
        return service;
    }
}
