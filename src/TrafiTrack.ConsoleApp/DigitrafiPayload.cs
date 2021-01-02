using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrafiTrack.ConsoleApp
{
    public sealed class DigitrafiPayload
    {
        public DigitrafiVehiclePosition VP { get; set; }
    }

    public sealed class DigitrafiVehiclePosition
    {
        [JsonProperty("desi")]
        public string RouteNumber { get; set; }

        [JsonProperty("dir")]
        public string RouteDirection { get; set; }

        [JsonProperty("tst")]
        public DateTimeOffset VehicleTimestamp { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
        
        [JsonProperty("long")]
        public double Longtitude { get; set; }

        

    }
}
