using System;
using System.Collections.Generic;
using System.Text;

namespace WordCloud.Proxy
{
    public class ListItem
    {
        /// <summary>
        ///  
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
    }

    public class DataItem
    {
        /// <summary>
        /// 微博 热搜榜
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }
    }

    public class NewsNode
    {
        /// <summary>
        /// 
        /// </summary>
        public List<DataItem> data { get; set; }
    }
}
