using OpenQA.Selenium.Chrome;
using System.Threading;

namespace WordCloud.Proxy
{
    public class ChromeAgent
    {
        public void Start(string url)
        { 
            var chromeOptions = new ChromeOptions(); 
            var driver = new ChromeDriver(chromeOptions);
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(50000); 
            driver.Quit();
        }
    }
}
