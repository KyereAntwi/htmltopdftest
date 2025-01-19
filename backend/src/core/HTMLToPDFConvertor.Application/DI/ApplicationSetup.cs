using HTMLToPDFConvertor.Application.Features.Convertor.Commands.ConvertHtmlToPdf;
using Microsoft.Extensions.DependencyInjection;

namespace HTMLToPDFConvertor.Application.DI;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(req =>
        {
            req.RegisterServicesFromAssemblyContaining<ConvertHtmlToPdfCommandHandler>();
        });
        return services;
    }
}