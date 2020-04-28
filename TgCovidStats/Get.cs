using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using TgCovidStats.Data;
using TgCovidStats.Model;

namespace TgCovidStats
{
    public static class Get
    {
        public static async Task<List<Details>>LocalDetailsAsync()
        {

            if (Local.ReadDetails().Count > 0)
            {
                return Local.ReadDetails();
            }
            else
            {
                var returnValue = await DetailsAsync();
                Local.SaveDetails(returnValue);
                return returnValue;
            }

        }

        public static async Task<Stats> LocalStatAsync()
        {

            if (Local.ReadStats() != null)
            {
                return Local.ReadStats();
            }
            else
            {
                var returnValue = await StatAsync();
                Local.SaveStat(returnValue);
                return returnValue;
            }

        }
        public  static async Task<List<Details>> DetailsAsync()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync("https://covid19.gouv.tg/situation-au-togo/");
            var details = new List<Details>();

            var sections = document.QuerySelectorAll(".ee-loop__item>article>div>div>div");
            string xt = string.Empty;

            foreach (var item in sections.Skip(1))
            {
                var itemDetails = new Details();
                xt += "\n\n";
                var itemsections = item.QuerySelectorAll("section");
                var itemHtmlDetails = itemsections.FirstOrDefault().QuerySelectorAll("h2");
             

                int i = 0;
                Stats itemStats = new Stats();
                   itemStats.TimeInfo = $"{itemHtmlDetails[0].InnerHtml} à { itemHtmlDetails[1].InnerHtml}";
                itemStats.ActiveCases = itemHtmlDetails[3].InnerHtml.GetInt();
                itemStats.Cured = itemHtmlDetails[4].InnerHtml.GetInt();
                itemStats.Deaths = itemHtmlDetails[5].InnerHtml.GetInt();
                itemDetails.Stat = itemStats;

                itemDetails.Histoire = Regex.Replace(itemsections[1].TextContent, @"^[\s\t\n]+|[\s\t\n]+$", "");
                itemDetails.Histoire = Regex.Replace(itemDetails.Histoire, @"<[^>]+>|&nbsp;", "").Trim();
                itemDetails.Histoire = Regex.Replace(itemDetails.Histoire, @"\s{2,}", " "); ;
                details.Add(itemDetails);

            }

            return details;
        }
        public  static async Task<Stats> StatAsync()
        {
            // Load default configuration
            var config = Configuration.Default.WithDefaultLoader();
            // Create a new browsing context
            var context = BrowsingContext.New(config);
            // This is where the HTTP request happens, returns <IDocument> that // we can query later
            var document = await context.OpenAsync("http://covid19.gouv.tg/");
            var stat = new Stats();
            stat.ActiveCases = document.ReadInteger("#active-cases>div>h2");
            stat.Cured = document.ReadInteger("#cured>div>h2");
            stat.Deaths = document.ReadInteger("#deceased>div>h2");
            stat.TimeInfo = StripTagsRegexCompiled(document.QuerySelector("#timeinfo").InnerHtml);
            // stat.timeInfo=Regex.Replace( @"\W+", "");
            stat.TimeInfo = Regex.Replace(stat.TimeInfo, "[^0-9A-Za-z à ,:]", "");
            return stat;
        }

        private static string StripTagsRegexCompiled(string innerHtml)
        {
            throw new NotImplementedException();
        }
    }
}
