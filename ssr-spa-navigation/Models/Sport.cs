using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ssr_spa_navigation.Models
{
    public class Sport
    {
        [JsonProperty("id")]
        public  string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("leagues")]
        public List<League> Leagues { get; set; }

        public Sport()
        {
            Leagues = new List<League>();
        }
    }

    public class League
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public  string  Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
