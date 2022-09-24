using MQTTnet.Server;
using MQTTnet;
using System.Text;

namespace csMqttServer;

internal class Program
{
    private static IMqttServer _mqttServer;

    static void Main(string[] args)
    {

        // 設定 MQTT server 的運作環境參數
        var optionsBuilder = new MqttServerOptionsBuilder()
            .WithConnectionBacklog(100)
            .WithDefaultEndpointPort(1884)
            .WithMaxPendingMessagesPerClient(1000);

        // 透過 MqttFactory 這個工廠方法類別，建立一個 MQTT 伺服器物件
        _mqttServer = new MqttFactory().CreateMqttServer();

        // 綁定當訊息送到此伺服器之後的事件，定義該做甚麼事情
        _mqttServer.UseApplicationMessageReceivedHandler(async e =>
        {
            // 對於此訊息的 Payload 部分，將會是經過編碼的，因此需要先解碼
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            var topic = e.ApplicationMessage.Topic;
            if (topic == "Hi")
            {
                Console.WriteLine("subscription message received");
                Console.WriteLine("Simulating Say Hello messages...");
                await SimulateSayHelloAsync();
            }
        });

        // 一旦有用戶端連線上來之後，將會觸發這個事件
        _mqttServer.UseClientConnectedHandler(e =>
        {
            Console.WriteLine("***** CLIENT CONNECTED : " + e.ClientId + " *******");
        });

        // 啟動這個 MQTT 伺服器
        _mqttServer.StartAsync(optionsBuilder.Build());

        Console.ReadLine();
    }

    private static async Task SimulateSayHelloAsync()
    {
        await PublishMessage("HiEcho", "Hello Word!");
    }

    private static async Task PublishMessage(string topic, string message)
    {
        // 產生一個 MQTT 訊息
        var mqttMessage = new MqttApplicationMessageBuilder()
                            .WithTopic(topic)
                            .WithPayload(message)
                            .WithAtLeastOnceQoS()
                            .WithRetainFlag(false)
                            .WithDupFlag(false)
                            .Build();

        // 發佈此剛剛建立的訊息
        var result = await _mqttServer.PublishAsync(mqttMessage, CancellationToken.None);

        if (result.ReasonCode == MQTTnet.Client.Publishing.MqttClientPublishReasonCode.Success)
            Console.WriteLine("Message published : " + message);
    }
}
