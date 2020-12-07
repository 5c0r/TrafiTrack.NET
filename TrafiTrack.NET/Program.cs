using MQTTnet;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TrafiTrack.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var mqttFactory = new MqttFactory();
                var mqttClient = mqttFactory.CreateMqttClient();

                var mqttOptions = new MqttClientOptionsBuilder()
                    .WithWebSocketServer("mqtt.hsl.fi")
                    .WithTls()
                    .Build();

                // /hfp/v2/journey/ongoing/vp/bus/0022/00986/4561/1/Lentoasema/20:01/4510224/5/60;24/29/85/89
                mqttClient.ConnectedHandler = new TrafiTrackConnectedHandler(mqttClient);
                mqttClient.ApplicationMessageReceivedHandler = new TrafiTrackOnReceivedHandler(mqttClient);
                mqttClient.DisconnectedHandler = new TrafiTrackDisconnectedHandler(mqttClient);

                await mqttClient.ConnectAsync(mqttOptions, CancellationToken.None);

                // Escape with any keys
                Console.ReadLine();

                var mqttDisconnectOptions = new MqttClientDisconnectOptions();
                mqttDisconnectOptions.ReasonCode = MqttClientDisconnectReason.AdministrativeAction;
                mqttDisconnectOptions.ReasonString = "I do";

                await mqttClient.DisconnectAsync(mqttDisconnectOptions, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0}", e.Message);
                throw;
            }
        }
    }
}
