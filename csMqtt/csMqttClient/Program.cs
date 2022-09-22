using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet;
using System.Text;
using MQTTnet.Client.Subscribing;

namespace csMqttClient;

internal class Program
{
    private static IMqttClient _mqttClient;

    static void Main(string[] args)
    {
        // 使用 MqttFactory 工廠類別，建立一個 MQTT 用戶端的物件
        _mqttClient = new MqttFactory().CreateMqttClient();

        // 每個用戶端都要有唯一的 Id，並且要再宣告 MQTT 伺服器的連線位址與方式
        var options = new MqttClientOptionsBuilder().WithClientId("MqttClient")
                                                    .WithTcpServer("localhost", 1884)
                                                    .Build();

        // 綁定當此用戶端已經連線到遠端伺服器之後，所要處理的工作
        _mqttClient.UseConnectedHandler(async e =>
        {
            // 在此訂閱有興趣的主題訊息
            MqttClientSubscribeResult subResult =
            await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
            .WithTopicFilter("HiEcho")
            .Build());

            // 對伺服器送出問好訊息
            PublishMessage("Hi", "Test Message");
        });

        // 綁定當訊息傳送到此用戶端之後，將會觸發的事件
        _mqttClient.UseApplicationMessageReceivedHandler(e =>
        {
            Console.WriteLine($"來自伺服器的問安 {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
        });

        // 連線到遠端伺服器上
        _mqttClient.ConnectAsync(options, CancellationToken.None);

        Console.Read();
    }

    private static async void PublishMessage(string topic, string message)
    {
        // Create mqttMessage
        var mqttMessage = new MqttApplicationMessageBuilder()
                            .WithTopic(topic)
                            .WithPayload(message)
                            .WithExactlyOnceQoS()
                            .Build();

        // Publish the message asynchronously
        await _mqttClient.PublishAsync(mqttMessage, CancellationToken.None);
    }
}
