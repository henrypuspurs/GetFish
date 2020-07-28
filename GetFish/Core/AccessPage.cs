using GetFish.Core.Models;
using GetFish.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace GetFish.Core
{
    class AccessPage
    {
        public static void UrlConstructor(int capture)
        {
            List<Fish> fishlist = new List<Fish>();
            WebClient client = new WebClient();
            WebPage thisPage = new WebPage();
            string pre = "https://www.thesiteIused.se/Summary/";
            int post = 1;
            int invalidCount = 0;
            for (int validCount = 0; validCount < capture;)
            {
                thisPage.Url = pre + post.ToString();
                thisPage.Html = GetPageBody(client.DownloadString(thisPage.Url));

                if (thisPage.Html != null)
                {
                    Fish thisfish = ExtractInfo.ExtractDetails(thisPage);
                    fishlist.Add(thisfish);
                    validCount++;
                    Console.Write($"\rFishing: {validCount} Fish Caught");
                    invalidCount = 0;
                }
                else if (invalidCount >= 10000)
                {
                    break;
                }
                else
                {
                    invalidCount++;
                }
                post++;
            }

            SaveToText.SaveToFile(fishlist);
        }

        //private static WebPage GetPage(string target = null)
        //{
        //    var getsite = new WebPage();
        //    if (target != null)
        //    {
        //        getsite.Url = target;
        //    }
        //    string url = getsite.Url;
        //    var client = new WebClient();
        //    var html = client.DownloadString(url);
        //    getsite.Html = GetPageBody(html);
        //    return getsite;
        //}

        private static string GetPageBody(string html)
        {
            string pattern = @".+";

            MatchCollection results = Regex.Matches(html, pattern);
            List<string> output = new List<string>();
            bool foundTop = false;

            foreach (Match result in results)
            {
                string holdresult = result.Value.Trim();

                if (foundTop == false && holdresult.Contains("ss-sciname"))
                {
                    foundTop = true;
                }
                else if (foundTop == true && holdresult.Contains("ss-moreinfo-container"))
                {
                    break;
                }
                else if (foundTop == true && holdresult.Length > 0)
                {
                    output.Add(holdresult);
                }
            }

            string outputString = string.Join("\n", output);
            if (foundTop == false)
            {
                outputString = null;
            }
            return outputString;
        }
    }
}
