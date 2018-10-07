using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ssr_spa_navigation.Models;

namespace ssr_spa_navigation.ViewModels
{
    public class ArticleDetailsVM
    {
        [JsonProperty("article")]
        public Article Article { get; set; }

        [JsonProperty("betofday")]
        public  string Betofday { get; set; }

        public ArticleDetailsVM()
        {
            Article = new Article();
        }
    }
}
