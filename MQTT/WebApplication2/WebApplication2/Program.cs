using MQTTnet.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// This adds a hosted mqtt server to the services
builder.Services.AddHostedMqttServer(mqttServer => mqttServer.WithoutDefaultEndpoint())
            .AddMqttConnectionHandler()
            .AddConnections(); ;

//builder.Services.AddHostedMqttServer(builder =>
//{
//    builder.WithDefaultEndpointPort(1883);
//    builder.WithEncryptedEndpointPort(8883);
//    builder.with
//});
//// This adds TCP server support based on System.Net.Socket
//builder.Services.AddMqttTcpServerAdapter();

//// This adds websocket support
//builder.Services.AddMqttWebSocketServerAdapter();

//builder.Services.AddMqttConnectionHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapMqtt("/mqtt");
app.UseMqttServer(server =>
{
    // Todo: Do something with the server
});

app.Run();
