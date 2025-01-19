using HTMLToPDFConvertor.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .Configure()
    .ConfigurePipeline();

app.Run();