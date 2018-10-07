using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ssr_spa_navigation.ViewModels
{
    public class HeaderVM
    {
        [JsonProperty("header")]
        public  string Header { get; set; }
    }
}
