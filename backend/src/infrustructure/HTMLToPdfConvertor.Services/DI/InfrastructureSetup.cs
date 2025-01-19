using HTMLToPDFConvertor.Application.Contracts;
using HTMLToPdfConvertor.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HTMLToPdfConvertor.Services.DI;

public static class InfrastructureSetup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IConvertor, Convertor>();
        return services;
    }
}