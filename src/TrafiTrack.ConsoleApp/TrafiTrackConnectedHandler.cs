using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using System.Threading.Tasks;

namespace TrafiTrack.ConsoleApp
{
    public sealed class TrafiTrackConnectedHandler : IMqttClientConnectedHandler
    {
        private readonly IMqttClient mqttClient;
        private readonly string topic = "/hfp/v2/journey/ongoing/vp/tram/#";

        public TrafiTrackConnectedHandler(IMqttClient mqttClient)
        {
            this.mqttClient = mqttClient;
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            System.Console.WriteLine("Connected");

            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
            System.Console.WriteLine("Subscribed?");
        }
    }
}
