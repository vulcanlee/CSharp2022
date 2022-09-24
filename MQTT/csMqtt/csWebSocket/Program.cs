using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using System.Text;

namespace csWebSocket
{
    internal class Program
    {
        private static IMqttClient _mqttClient;

        static async Task Main(string[] args)
        {
            // Create client
            _mqttClient = new MqttFactory().CreateMqttClient();
            var options = new MqttClientOptionsBuilder().WithClientId("MqttClient")
                                                        .WithWebSocketServer("localhost:5059/mqtt")
                                                        //.WithTcpServer("localhost", 1883)
                                                        .Build();
            // When client connected to the server
            _mqttClient.UseConnectedHandler(async e =>
            {
                // Subscribe to a topic
                MqttClientSubscribeResult subResult = await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                                                                       .WithTopicFilter("mqttServerTopic")
                                                                       .Build());
                // Sen a test message to the server
                PublishMessage("Test Message");
            });

            // When client received a message from server
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            });

            // Connect ot server
            _mqttClient.ConnectAsync(options, CancellationToken.None);

            Console.Read();
        }

        private static async void PublishMessage(string message)
        {
            // Create mqttMessage
            var mqttMessage = new MqttApplicationMessageBuilder()
                                .WithTopic("mqttServerTopic")
                                .WithPayload(message)
                                .WithExactlyOnceQoS()
                                .Build();

            // Publish the message asynchronously
            await _mqttClient.PublishAsync(mqttMessage, CancellationToken.None);
        }
    }
}