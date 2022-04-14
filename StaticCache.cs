using System.Collections.Generic;

namespace WordCloud.Proxy
{
    public static class StaticCache
    {
        public static NewsNode Newses { get; set; }

        public static Dictionary<string, int> Keywords { get; set; }
    }
}
