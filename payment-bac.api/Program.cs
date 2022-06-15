using Microsoft.Data.SqlClient;
using NServiceBus;
using Microsoft.EntityFrameworkCore;
using payment_bac.api.Aggregates;
using payment_bac.api.Data;
using payment_bac.api.DataAccess;
using payment_bac.api.Handlers;
using payment_bac.domain.messages;
using payment_bac.api.Utilities;

var builder = WebApplication.CreateBuilder(args);
var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var rabbitConnectionString = (string) builder.Configuration.GetSection("RabbitMqSettings").GetValue(typeof(string), "ConnectionString");

// Add services to the container.
builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddDbContext<Context>(options => 
    options.UseSqlServer(sqlConnectionString));

// Configure the host to have an NServiceBus endpoint
builder.Host.UseNServiceBus(context =>
{
    var endpointName = "payment-bac.api";
    var endpointConfiguration = new EndpointConfiguration(endpointName);

    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.SqlDialect<SqlDialect.MsSqlServer>();
    persistence.ConnectionBuilder(connectionBuilder: () =>
    {
        return new SqlConnection(sqlConnectionString);
    });

    var transport = endpointConfiguration
        .UseTransport<RabbitMQTransport>()
        .ConnectionString(rabbitConnectionString)
        .UseConventionalRoutingTopology();

    transport.Routing().RouteToEndpoint(typeof(PaymentFinished), endpointName);

    return endpointConfiguration;
});

// Add my custom services to the container.
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddSingleton<ISessionIdGenerator, SessionGUIDGenerator>();
builder.Services.AddSingleton<IPolicyGenerator, MockPolicyGenerator>();

builder.Services.AddScoped<IPolicyBrowser, PolicyBrowser>();
builder.Services.AddScoped<ISessionCreator, SessionCreator>();
builder.Services.AddScoped<ISessionHandler, SessionHandler>();

builder.Services.AddScoped<IPaymentRaiser, PaymentRaiser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Run();