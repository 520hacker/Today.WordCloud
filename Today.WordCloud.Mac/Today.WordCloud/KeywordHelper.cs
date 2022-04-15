using JiebaNet.Analyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCloud.Proxy
{
    public class KeywordHelper
    {
        public static List<string> blackList = new List<string>() {
            "问题","一位","ty","直接","心里",
            "记录","是不是","怎么","讨论","回复","集团","一期","没有",
            "不是","不能","热度","10W","大家","小时","我们","少数派",
            "月销","热销","原价","券后","阅读","回应","内容","最后","新闻"
            };
        public static async Task CreateKeywordDicAsync()
        {
            var max = 1;
            Dictionary<string, int> keywords = new Dictionary<string, int>();
            var list = await GetKeywordsAsync(StaticCache.Newses);
            var disList = list.Distinct().ToList();
            foreach (var item in disList)
            {
                keywords[item] = list.Count(o => o == item);
                if (keywords[item] > max)
                {
                    max = keywords[item] + 1;
                }
            }

            keywords[Week()] = max - 3;
            keywords[DateTime.Now.ToString("yyyy")] = max - 6;
            keywords[DateTime.Now.ToString("MM月")] = max - 4;
            keywords[DateTime.Now.ToString("dd日")] = max;
            StaticCache.Keywords = keywords;
        }

        public static string Week()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];

            return week;
        }
        public static async Task<List<string>> GetKeywordsAsync(NewsNode newses)
        {
            var keywords = new List<string>();



            List<Task<List<string>>> downloadTasks = new List<Task<List<string>>>();
            // 到这里，全部的任务已经开始执行了。
            // 用异步方式等待全部下载完成。


            foreach (var newsItem in newses.data.Take(StaticCache.MessageListNum))
            {
                var task = Task.Run(() =>
                {
                    return GetKeywords(newsItem.list.Select(o => o.title).ToList());
                });
                downloadTasks.Add(task);
            }

            List<string>[] lists = await Task.WhenAll(downloadTasks);
            foreach (var list in lists)
            {
                keywords.AddRange(list);
            }

            return keywords.Where(o => !blackList.Contains(o)).ToList();
        }

        public static async Task<List<string>> GetKeywords(List<string> strs)
        {
            var keywords = new List<string>();
            foreach (string str in strs)
            {
                var list = GetKeywords(str);
                keywords.AddRange(list);
            }

            System.Console.WriteLine($"找到{keywords.Count}个关键字！");
            return keywords;
        }

        public static List<string> GetKeywords(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new List<string>();
            }

            var t = 0m;
            TfidfExtractor tfid = new TfidfExtractor();
            var tags = tfid.ExtractTags(str, 20).ToList();
            tags = tags.Where(o => !string.IsNullOrWhiteSpace(o) &&
                !decimal.TryParse(o, out t) && !blackList.Contains(o)).ToList();
            System.Console.WriteLine($"找到关键字：{string.Join(",", tags)}");
            return tags;
        }
    }
}
