using HTMLToPDFConvertor.Application.DI;
using HTMLToPdfConvertor.Services.DI;

namespace HTMLToPDFConvertor.Api;

public static class Startup
{
    public static WebApplication Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddInfrastructure();
        builder.Services.AddApplication();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", b => b
                .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
        });

        
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseCors("Open");
        return app;
    }
}