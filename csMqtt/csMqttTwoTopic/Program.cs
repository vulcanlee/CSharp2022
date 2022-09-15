using MQTTnet.Server;
using MQTTnet;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet.Client.Subscribing;

namespace csMqttTwoTopic
{
    public enum MessageTopicEnum
    {
        TopicTitle1,
        TopicTitle2,
        TopicTitle3,
        TopicTitle4,
        TopicTitle5,
    }
    public class Program
    {
        private static IMqttServer _mqttServer;
        static List<Task> _tasks = new List<Task>();
        static object _lock = new object();
        static int Total = 0;
        static string sendTopic = @"CheckinKiosk\301\ClinicStartInfos";
        static string sendTopic2 = @"CheckinKiosk\301\RegisterData";

        static async Task Main(string[] args)
        {
            Console.WriteLine($"Broker 端開始啟動");
            //Broker();

            #region Client Init
            Console.WriteLine($"準備進行用戶端的初始化");
            //Client("ClientA", MessageTopicEnum.TopicTitle2.ToString());
            //Client("ClientB", MessageTopicEnum.TopicTitle5.ToString());
            #endregion

            var myClient = ClientSimulator("xxxx", sendTopic);

            // for Debug
            Console.WriteLine($"Delay 3 seconds");
            await Task.Delay(5000);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"送出第一個訊息");
                await myClient.PublishAsync(new MqttApplicationMessage()
                {
                    Topic = sendTopic,
                    Payload = Encoding.UTF8.GetBytes($"{DateTime.Now} - {DateTime.Now.Ticks}"),
                });
                Console.WriteLine($"送出第二個訊息");
                await myClient.PublishAsync(new MqttApplicationMessage()
                {
                    Topic = sendTopic2,
                    Payload = Encoding.UTF8.GetBytes($"{DateTime.Now} - {DateTime.Now.Ticks}"),
                });
            }

            #region Loop Testing
            //for (int i = 0; i < 1000; i++)
            //{
            //    Console.WriteLine($"*** 即將從伺服器內連續送出兩個訊息");
            //    #region Send Message
            //    sendTopic = MessageTopicEnum.TopicTitle1.ToString();
            //    Console.WriteLine($"@ Server 送出 {sendTopic} 訊息");
            //    await PublishMessage(sendTopic,
            //        $"{DateTime.Now} - {DateTime.Now.Ticks}");
            //    #endregion

            //    #region Send Message
            //    sendTopic = MessageTopicEnum.TopicTitle5.ToString();
            //    Console.WriteLine($"@ Server 送出 {sendTopic} 訊息");
            //    await PublishMessage(sendTopic,
            //        $"{DateTime.Now} - {DateTime.Now.Ticks}");
            //    #endregion

            //    #region Send Message
            //    sendTopic = MessageTopicEnum.TopicTitle2.ToString();
            //    Console.WriteLine($"@ Server 送出 {sendTopic} 訊息");
            //    await PublishMessage(sendTopic,
            //        $"{DateTime.Now} - {DateTime.Now.Ticks}");
            //    #endregion
            //}
            #endregion

            Console.ReadLine();
        }

        #region Broker 的服務初始化與自動觸發送出訊息
        static void Broker()
        {
            // Configure MQTT server.
            var optionsBuilder = new MqttServerOptionsBuilder()
                //.WithConnectionBacklog(100)
                .WithDefaultEndpointPort(1884)
                //.WithMaxPendingMessagesPerClient(1000)
                ;

            // Define a mqttServer
            _mqttServer = new MqttFactory().CreateMqttServer();

            // Message arrived configuration
            _mqttServer.UseApplicationMessageReceivedHandler(async e =>
            {
                var foo = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                Console.WriteLine($"[Broker] subscription message received");
            });

            // When a new client connected
            _mqttServer.UseClientConnectedHandler(e =>
            {
                Console.WriteLine($"[Broker] ***** CLIENT CONNECTED : " + e.ClientId + " *******");
            });

            // Start the mqtt server
            _mqttServer.StartAsync(optionsBuilder.Build());
        }
        private static async Task PublishMessage(string topic, string message)
        {
            // Create mqttMessage
            var mqttMessage = new MqttApplicationMessageBuilder()
                                .WithTopic(topic)
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
        private static async Task PublishMessageSimulate(string topic, string message)
        {
            // Create mqttMessage
            var mqttMessage = new MqttApplicationMessageBuilder()
                                .WithTopic(topic)
                                .WithPayload(message)
                                //.WithAtLeastOnceQoS()
                                //.WithRetainFlag(false)
                                //.WithDupFlag(false)
                                .Build();

            // Publish the message asynchronously
            var result = await _mqttServer.PublishAsync(mqttMessage, CancellationToken.None);

            if (result.ReasonCode == MQTTnet.Client.Publishing.MqttClientPublishReasonCode.Success)
                Console.WriteLine("Message published : " + message);
        }
        #endregion
        static IMqttClient ClientSimulator(string ClientId, string MonitorTopic = "")
        {
            IMqttClient _mqttClient;
            // Create client
            _mqttClient = new MqttFactory().CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                .WithClientId(ClientId)
                .WithTcpServer("192.168.82.12", 1883)
                .WithCredentials(new MqttClientCredentials()
                {
                    Username = "UserName",
                })
                .Build();
            // When client connected to the server
            _mqttClient.UseConnectedHandler(async e =>
            {
                // Subscribe to a topic
                Console.WriteLine($"Client[{ClientId}] Subscribe Topic[{MonitorTopic}]");
                MqttClientSubscribeResult subResult =

                await _mqttClient
                .SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter(MonitorTopic)
                .Build());

                await _mqttClient
                .SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter(sendTopic2)
                .Build());
            });

            // When client received a message from server
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Interlocked.Increment(ref Total);
                string foo = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                string bar = e.ApplicationMessage.Topic;
                Console.WriteLine($"    ?? Client[{ClientId}] {Total} 接收到 [{bar}] Payload = {foo}");
                Console.WriteLine();
            });

            // Connect ot server
            _mqttClient.ConnectAsync(options, CancellationToken.None);
            return _mqttClient;
        }

        #region 用戶端訂閱主題與反應要顯示出所接收到的訊息
        static void Client(string ClientId, string MonitorTopic = "")
        {
            var task = Task.Run(async () =>
             {
                 IMqttClient _mqttClient;
                 // Create client
                 _mqttClient = new MqttFactory().CreateMqttClient();
                 var options = new MqttClientOptionsBuilder().WithClientId(ClientId)
                                                             .WithTcpServer("192.168.82.12", 1883)
                                                             .WithCredentials(new MqttClientCredentials()
                                                             {
                                                                 Username = "UserName",
                                                             })
                                                             .Build();
                 // When client connected to the server
                 _mqttClient.UseConnectedHandler(async e =>
                 {
                     // Subscribe to a topic
                     Console.WriteLine($"Client[{ClientId}] Subscribe Topic[{MonitorTopic}]");
                     MqttClientSubscribeResult subResult =
                     await _mqttClient
                     .SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                     .WithTopicFilter(MonitorTopic)
                     .Build());
                 });

                 // When client received a message from server
                 _mqttClient.UseApplicationMessageReceivedHandler(e =>
                 {
                     Interlocked.Increment(ref Total);
                     string foo = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                     string bar = e.ApplicationMessage.Topic;
                     Console.WriteLine($"  Client[{ClientId}] {Total} 接收到 [{bar}] Payload = {foo}");
                 });

                 // Connect ot server
                 _mqttClient.ConnectAsync(options, CancellationToken.None);

                 while (true) { await Task.Delay(100); }
             });
            _tasks.Add(task);
        }
        #endregion
    }
}