using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ssr_spa_navigation.Models;

namespace ssr_spa_navigation.ViewModels
{
    public class OffersVM
    {
        [JsonProperty("offers")]
        public List<Offer> Offers { get; set; }

        public OffersVM()
        {
            Offers = new List<Offer>();
        }
    }
}
