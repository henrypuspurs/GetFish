using GetFish.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GetFish.Core
{
    public class ExtractInfoOld
    {
        public static Fish ExtractDetails(WebPage page)
        {
            string pageBody = GetPageBody(page);

            List<string> patterns = new List<string>{
            @"(?<=sciname)(.*?)(?<=Genera)(.*?)(?<=>)(?<info>.*?)</a>",
            @"(?<=sciname)(.*?)(?<=Species)(.*?)(?<=>)(?<info>.*?)</a>",
            @"(?<=sheader2)(.*?)\s(?<info>.*?)\s*</span>"
            };

            List<string> matches = new List<string>();

            foreach(string pattern in patterns)
            {
                var details = PageResults(pageBody, pattern);
                foreach (string detail in details)
                {
                    matches.Add(detail);
                }
            }

            Fish fish = new Fish();
            fish.Name = matches[2];
            fish.TName = matches[0] + " " + matches[1];
            //fish.Type = ;
            //fish.Details = ;
            //fish.Distribution = ;
            //fish.Length = ;
            //fish.RangePH = ;
            //fish.RangeTemp = ;
            //fish.RangedkH = ;


            return fish;
        }

        static string GetPageBody(WebPage page)
        {
            string pattern = @".+";

            MatchCollection results = Regex.Matches(page.Html, pattern);
            List<string> output = new List<string>();
            bool foundTop = false;
            bool foundBottom = false;
            foreach (Match result in results)
            {
                string holdresult = result.Value.Trim();

                if (holdresult.Contains("ss-sciname"))
                {
                    foundTop = true;
                }

                if (holdresult.Contains("ss-moreinfo-container"))
                {
                    foundBottom = true;
                }

                if (holdresult.Length > 0 && foundTop == true && foundBottom == false)
                {
                    output.Add(holdresult);
                }
            }

            string outputString = string.Join("\n", output);
            return outputString;
        }

        static List<string> PageResults(string page, string pattern)
        {
            MatchCollection results = Regex.Matches(page, pattern);
            List<string> matches = new List<string>();
            foreach (Match name in results)
            {
                try
                {
                    matches.Add(name.Groups["info"].Value);
                }
                catch

                {
                    matches.Add(name.Value);
                }
            }
            if (matches.Count == 0)
            {
                matches.Add("Not Found");
            }
            return matches;
        }

    }
}
