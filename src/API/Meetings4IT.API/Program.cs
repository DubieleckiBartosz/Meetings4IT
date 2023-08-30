using Meetings4IT.Shared.Implementations;
using System.Reflection;
using Meetings4IT.Shared.Implementations.Logging;
using Panels.Application;
using Serilog;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess; 
using Meetings4IT.API.Configurations;
using Meetings4IT.API.Modules;
using Notifications.Core.Reference;
using Identities.Core.Reference;
using Meetings4IT.Shared.Implementations.Modules;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.RegisterIdentityModule();

//Yes, we can write some dynamic method which could read all needed assemblies, but in this case we have control over it
var assemblyTypes = new Type[]
{
    typeof(PanelAssemblyReference),
    typeof(NotificationAssemblyReference),
    typeof(IdentityAssemblyReference)
};
 
var integrationEventTypes = assemblyTypes
    .Select(_ => Assembly.Load(_.Assembly.FullName!)
        .GetTypes()).SelectMany(t => t)
    .Where(t => t.IsSubclassOf(typeof(IntegrationEvent))).ToList(); 


builder.RegistrationSharedConfigurations(integrationEventTypes, assemblyTypes: assemblyTypes);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((ctx, lc) => lc.LogConfigurationService(builder.Configuration));
builder.SwaggerConfiguration();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.InitDataIdentityModule(configuration);
app.RegisterNotificationEvents();

app.Run();
