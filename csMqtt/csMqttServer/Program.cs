using MQTTnet.Server;
using MQTTnet;
using System.Text;

namespace csMqttServer;

internal class Program
{
    private static IMqttServer _mqttServer;

    static void Main(string[] args)
    {

        // Configure MQTT server.
        var optionsBuilder = new MqttServerOptionsBuilder()
            .WithConnectionBacklog(100)
            .WithDefaultEndpointPort(1884)
            .WithMaxPendingMessagesPerClient(1000);

        // Define a mqttServer
        _mqttServer = new MqttFactory().CreateMqttServer();

        // Message arrived configuration
        _mqttServer.UseApplicationMessageReceivedHandler(async e =>
        {
            if (Encoding.UTF8.GetString(e.ApplicationMessage.Payload) == "Test Message")
            {
                Console.WriteLine("subscription message received");
                Console.WriteLine("Simulating messages...");
                await Simulate();
            }
        });

        // When a new client connected
        _mqttServer.UseClientConnectedHandler(e =>
        {
            Console.WriteLine("***** CLIENT CONNECTED : " + e.ClientId + " *******");
        });

        // Start the mqtt server
        _mqttServer.StartAsync(optionsBuilder.Build());

        Console.ReadLine();
    }

    private static async Task PublishMessage(string message)
    {
        // Create mqttMessage
        var mqttMessage = new MqttApplicationMessageBuilder()
                            .WithTopic("mqttServerTopic")
                            .WithPayload(message)
                            .WithAtLeastOnceQoS()
                            .WithRetainFlag(false)
                            .WithDupFlag(false)
                            .Build();

        // Publish the message asynchronously
        var result = await _mqttServer.PublishAsync(mqttMessage, CancellationToken.None);

        if (result.ReasonCode == MQTTnet.Client.Publishing.MqttClientPublishReasonCode.Success)
            Console.WriteLine("Message published : " + message);
    }

    private static async Task Simulate()
    {
        for (int i = 0; i < 1000; i++)
        {
            var message = "This is a message from server " + i.ToString();
            await PublishMessage(message);
        }
    }
}
