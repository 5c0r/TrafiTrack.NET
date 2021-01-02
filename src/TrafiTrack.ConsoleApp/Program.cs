using MQTTnet;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using Marten;
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
                var pgConnection = "Host=localhost;Port=5432;Database=postgres;Username=postgres;password=postgres";

                var mqttFactory = new MqttFactory();
                var mqttClient = mqttFactory.CreateMqttClient();
                var documentStore = DocumentStore.For(opts =>
                   {
                       opts.Connection(pgConnection);
                       opts.AutoCreateSchemaObjects = AutoCreate.All;
                   });

                using(var session = documentStore.LightweightSession())
                {
                    var mqttOptions = new MqttClientOptionsBuilder()
                    .WithWebSocketServer("mqtt.hsl.fi")
                    .WithTls()
                    .Build();


                    mqttClient.ApplicationMessageReceivedHandler = new TrafiTrackOnReceivedHandler(mqttClient, session);

                    // /hfp/v2/journey/ongoing/vp/bus/0022/00986/4561/1/Lentoasema/20:01/4510224/5/60;24/29/85/89
                    mqttClient.ConnectedHandler = new TrafiTrackConnectedHandler(mqttClient);
                    mqttClient.DisconnectedHandler = new TrafiTrackDisconnectedHandler(mqttClient);

                    await mqttClient.ConnectAsync(mqttOptions, CancellationToken.None);

                    // Escape with any keys
                    Console.ReadLine();

                    var mqttDisconnectOptions = new MqttClientDisconnectOptions();
                    mqttDisconnectOptions.ReasonCode = MqttClientDisconnectReason.AdministrativeAction;
                    mqttDisconnectOptions.ReasonString = "I do";

                    await mqttClient.DisconnectAsync(mqttDisconnectOptions, CancellationToken.None);
                    await session.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0}", e.Message);
                throw;
            }
        }
    }
}
