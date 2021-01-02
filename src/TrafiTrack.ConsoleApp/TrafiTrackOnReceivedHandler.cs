using System;
using System.Text;
using System.Threading.Tasks;
using Marten;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Receiving;
using Newtonsoft.Json;

namespace TrafiTrack.ConsoleApp
{
    public sealed class TrafiTrackOnReceivedHandler : IMqttApplicationMessageReceivedHandler
    {
        // TODO : Not really needed here
        private readonly IMqttClient mqttClient;
        private readonly IDocumentSession documentSession;

        public TrafiTrackOnReceivedHandler(IMqttClient mqttClient, IDocumentSession documentSession)
        {
            this.mqttClient = mqttClient;
            this.documentSession = documentSession;
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            var jsonString = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            Console.WriteLine("Payload {0}", jsonString);
            var payload = JsonConvert.DeserializeObject<DigitrafiPayload>(jsonString);
            Console.WriteLine("Received {0} {1} {2}", payload.VP.Latitude, payload.VP.Longtitude);

            documentSession.Insert(payload);
        }
    }
}
