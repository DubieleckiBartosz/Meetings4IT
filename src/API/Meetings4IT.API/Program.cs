using Meetings4IT.Shared.Implementations;
using System.Reflection;
using Meetings4IT.Shared.Implementations.Logging;
using Panels.Application;
using Serilog;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Meetings4IT.Shared.IntegrationEvents.Reference;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var integrationEventTypes = Assembly.Load(typeof(IntegrationEventsAssemblyReference).Assembly.FullName!).GetTypes()
    .Where(t => t.IsSubclassOf(typeof(IntegrationEvent)))
    .ToList();

builder.RegistrationSharedConfigurations(integrationEventTypes, assemblyTypes: new Type[]
{
    typeof(PanelAssemblyReference)
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((ctx, lc) => lc.LogConfigurationService(builder.Configuration));
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = LogDetails.ReadHttpRequest;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
