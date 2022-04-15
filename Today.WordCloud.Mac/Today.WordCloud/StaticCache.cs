using System.Collections.Generic;
using System.IO;

namespace WordCloud.Proxy
{
    public static class StaticCache
    {
        public static NewsNode Newses { get; set; }

        public static Dictionary<string, int> Keywords { get; set; }

        public static bool IsMac = File.Exists(@"/System/Library/CoreServices/SystemVersion.plist");

        public static int MessageListNum = 16;
    }
}
