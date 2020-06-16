using GetFish.Core.Models;
using System.Collections.Generic;
using System.IO;

namespace GetFish.Data
{
    public class SaveToText
    {
        public static void SaveToFile(List<Fish> fishes)
        {
            string saveTo = @"G:\Users\Henry\Documents\fish.txt";
            string d = "¬"; //custom delimiter
            string headerText = $"Scientific Name{d}Common Name{d}Temp range Low °C{d}Temp range High °C{d}pH range Low{d}pH range High{d}dH range Low{d}dH range High{d}Max Length cm{d}Distribution{d}Details{d}Type{d}PageUrl";

            List<string> outputList = new List<string> { headerText };

            foreach (Fish fish in fishes)
            {
                string outputLine = $"{fish.TName}{d}{fish.Name}{d}{fish.TempRange[0]}{d}{fish.TempRange[1]}{d}{fish.PHrange[0]}{d}{fish.PHrange[1]}{d}{fish.DHrange[0]}{d}{fish.DHrange[1]}{d}{fish.MaxLength}{d}{fish.Distribution}{d}{fish.Details}{d}{fish.Type}{d}{fish.PageUrl}";
                outputList.Add(outputLine);
            }

            string output = string.Join("\n", outputList);
            File.WriteAllText(saveTo, output);
        }
    }
}
