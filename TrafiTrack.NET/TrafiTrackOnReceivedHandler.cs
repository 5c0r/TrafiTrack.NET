using System;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Receiving;

namespace TrafiTrack.ConsoleApp
{
    public sealed class TrafiTrackOnReceivedHandler : IMqttApplicationMessageReceivedHandler
    {
        private readonly IMqttClient mqttClient;

        public TrafiTrackOnReceivedHandler(IMqttClient mqttClient)
        {
            this.mqttClient = mqttClient;
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("Received something");
            Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
        }
    }
}
