using System;
using System.Collections.Generic;
using System.Linq;
// using System.Runtime.Remoting.Metadata.W3cXsd2001;


// Manually entered to assign class attributes to json attributes
using System.Text.Json;
using System.Text.Json.Serialization;

using System.Threading.Tasks;

namespace InternalForcesGUI.Models
{
    public class userInput
    {
        // Shortcut prop + tab can make fast class attributes
        [JsonPropertyName("ID")]
        public string ID { get; set; } 

        [JsonPropertyName("DistributedForce")]
        public float DistributedForce { get; set; }

        [JsonPropertyName("Distance1")]
        public float Distance1 { get; set; }

        [JsonPropertyName("AXComp")]
        public float AXComp { get; set; }

        [JsonPropertyName("AYComp")]
        public float AYComp { get; set; }

        [JsonPropertyName("Graph1")]
        public string Graph1 { get; set; }

        [JsonPropertyName("Graph2")]
        public string Graph2 { get; set; }

        [JsonPropertyName("Ratings")]
        public int[] Ratings { get; set; }

         /* 
         * ToString
         * Takes all object information and converts it back into text for json
         * Link syntax (goes to) 
         */
        public override string ToString() => JsonSerializer.Serialize<userInput>(this);
    }
}
