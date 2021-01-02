using System;
using System.Threading.Tasks;
using MQTTnet.Client;
using MQTTnet.Client.Disconnecting;

namespace TrafiTrack.ConsoleApp
{
    public sealed class TrafiTrackDisconnectedHandler : IMqttClientDisconnectedHandler
    {
        private readonly IMqttClient mqttClient;

        public TrafiTrackDisconnectedHandler(IMqttClient mqttClient)
        {
            this.mqttClient = mqttClient;
        }

        public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {
            Console.WriteLine("Disconnected !");
        }
    }
}
