using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssr_spa_navigation.Infrastructure
{
    public class StuctureRequirement
    {
        public string StoreProperty { get; set; }
        public string Alias { get; set; }

        public Func<object, object> function;
    }
}
