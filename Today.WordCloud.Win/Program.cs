using System;
using System.Net;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Http;
using Titanium.Web.Proxy.Models;

namespace WordCloud.Proxy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                var proxy = new WordCloudProxy();
                proxy.Start();
            });

            Task.Run(() =>
            {
                var agent = new ChromeAgent();
                agent.Start("https://tools.miku.ac/news/");
            });

            Console.WriteLine("任务结束!");
            Console.ReadLine();
        }
    }
}
