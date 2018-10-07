using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ssr_spa_navigation.ViewModels
{
    public class FooterVM
    {
        [JsonProperty("footer")]
        public string Footer { get; set; }
    }
}
