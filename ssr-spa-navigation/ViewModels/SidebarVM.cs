using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ssr_spa_navigation.Models;

namespace ssr_spa_navigation.ViewModels
{
    public class SidebarVM
    {
        [JsonProperty("offers")]
        public List<Sport> Offers { get; set; }

        public SidebarVM()
        {
            Offers = new List<Sport>();
        }
    }
}
