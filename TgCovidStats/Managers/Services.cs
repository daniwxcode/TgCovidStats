using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace TgCovidStats
{

    public static class Services
    {
        public static string StripTagsRegex(this string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
        public static uint ReadInteger(this IDocument doc, string value)
        {
            return doc.QuerySelector(value).TextContent.GetInt();
        }
        public static uint GetInt(this string nombre)
        {
            MatchCollection regxMatches = Regex.Matches(nombre, @"\d+");
            uint.TryParse(string.Join("", regxMatches), out uint n);
            return n;
        }
        public static uint ReadInteger(this IElement doc, string value)
        {
            MatchCollection regxMatches = Regex.Matches(doc.QuerySelector(value).InnerHtml, @"\d+");
            uint.TryParse(string.Join("", regxMatches), out uint n);
            return n;
        }
        
        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(this string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

    }
}
