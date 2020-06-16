using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GetFish.Core.Models
{
    public class Fish
    {
        private string name;
        private string tName;
        private string rangePH;
        private string rangedkH;
        private string rangeTemp;
        private string distribution;
        private string length;
        private string details;
        private string type;

        public string PageUrl { get; set; }

        public string Name
        {
            get { return name; }
            set { name = CleanString(value); }
        }

        public string TName
        {
            get { return tName; }
            set { tName = CleanString(value); }
        }
        public string RangePH
        {
            get { return rangePH; }
            set { rangePH = CleanString(value); }
        }

        public string RangedkH
        {
            get { return rangedkH; }
            set { rangedkH = CleanString(value); }
        }

        public string RangeTemp
        {
            get { return rangeTemp; }
            set { rangeTemp = CleanString(value); }
        }

        public string Distribution
        {
            get { return distribution; }
            set { distribution = CleanString(value); }
        }

        public string Length
        {
            get { return length; }
            set { length = CleanString(value); }
        }

        public string Details
        {
            get { return details; }
            set { details = CleanString(value); }
        }

        public string Type
        {
            get { return type; }
            set { type = CleanString(value); }
        }

        public List<string> DHrange
        {
            get { return GetNumbers(rangedkH); }
            private set { }
        }

        public List<string> PHrange
        {
            get { return GetNumbers(rangePH); }
            private set { }
        }

        public List<string> TempRange
        {
            get { return GetNumbers(rangeTemp); }
            private set { }
        }
        public string MaxLength
        {
            get
            {
                string result = Regex.Match(length, @".*(?=\s*cm)").Value.Trim();
                if (result == "")
                {
                    result = "Not Found";
                }
                return result;
            }
            private set { }
        }

        //methods

        private List<string> GetNumbers(string value)
        {
            List<string> results = new List<string>();
            var matches = Regex.Matches(value, @"([\d|\?]*\.?\d?)").SelectMany(x => x.Groups.Values.Skip(1).Select(g => g.Value.Trim()));

            foreach (string match in matches)
            {
                if (match != "")
                {
                    results.Add(match);
                }
            }
            if (results.Count < 2)
            {
                results.Add("Not Found");
                results.Add("Not Found");
            }

            return results;
        }

        private string CleanString(string value)
        {
            string one = Regex.Replace(value, @"\s+", " ");
            string two = Regex.Replace(one, @"\s$", "");
            string three = Regex.Replace(two, @"&deg;", "°");
            string four = Regex.Replace(three, @"\(Ref.*\)", "");
            string five = Regex.Replace(four, @"<.*>", "");
            string cleaned = Regex.Replace(five, @"\s\.", ".");

            //not working
            //string[] find = { @"\s+", @"\s$", @"&deg;", @"\(Ref.*\)", @"<.*>", @"\s\." };
            //string[] replace = { " ", "", "°", "", "", "." };
            //string cleaned = "";
            //for (int i = 0; i < find.Length; i++)
            //{
            //    cleaned = Regex.Replace(value, find[i], replace[i]);
            //}

            if (cleaned == "")
            {
                cleaned = "Not Found";
            }
            return cleaned;
        }
    }
}
