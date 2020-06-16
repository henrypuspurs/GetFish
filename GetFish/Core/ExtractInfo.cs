using GetFish.Core.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace GetFish.Core
{
    public class ExtractInfo
    {
        public static Fish ExtractDetails(WebPage page)
        {
            string pageBody = page.Html;
            Fish fish = new Fish
            {
                Name = GetValue(pageBody, @"<span class='sheader2'>[\n]*(.*)[\s]*</span>"),
                TName = GetValue(pageBody, @"<span\sclass='sciname'>+.+'>([A-Z]{1}[a-z\s]*)</a></b></span>|<span\sclass='sciname'>+.+'>([a-z]{1}[a-z\s]*)</a></b></span>"),
                Type = GetValue(pageBody, @"<span>\n(.*;\s.*);\s(?=pH|depth)"),
                Details = GetValue(pageBody, @"(?:</form>\n.*smallSpace.*\n<span>)\n(.*)(?=</span>)"),
                Distribution = GetValue(pageBody, @"(?<=smallSpace.*\n<span>\n)(.*)(?=<BR />\s*</span>\n</div>\n</p>\n<p>)"),
                Length = GetValue(pageBody, @"Max length\s:\s([\d.\s-A-Za-z\/]*);"),
                RangePH = GetValue(pageBody, @"pH range:\s([\d.|\?\s-]*)[;|.]"),
                RangeTemp = GetValue(pageBody, @"([\d|\?]*?&deg;[C|F] - [\d|\?]*&deg;[C|F])"),
                RangedkH = GetValue(pageBody, @"dH range:\s([\d|\?\s-]*)."),
                PageUrl = page.Url
            };

            ////test print only
            //Console.WriteLine("Details Retrieved:\n");
            //Console.WriteLine($"|{fish.Name}|");
            //Console.WriteLine($"|{fish.TName}|");
            //Console.WriteLine($"|{fish.Type}|");
            //Console.WriteLine($"|{fish.Details}|");
            //Console.WriteLine($"|{fish.Distribution}|");
            //Console.WriteLine($"|{fish.Length}|");
            //Console.WriteLine($"|{fish.RangePH}|");
            //Console.WriteLine($"|{fish.RangeTemp}|");
            //Console.WriteLine($"|{fish.RangedkH}|");


            return fish;
        }

        /// <summary>
        /// Get the matches for the Regular Express, loop through the matches and their groups, the first item in a group is the complete result from the regex, so ignore that.
        /// Then join all found results together into a single string, separating them with a space
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static string GetValue(string source, string pattern)
        => string.Join(' ', new Regex(pattern).Matches(source).SelectMany(x => x.Groups.Values.Skip(1).Select(g => g.Value.Trim())));



    }
}

