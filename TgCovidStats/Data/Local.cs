using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using TgCovidStats.Model;

namespace TgCovidStats.Data
{
    public static class Local
    {
        private static string DetailsFile = "Details.json";
        private static string StatFile = "Stat.json";


        private static string Read(string strResourceName)
        {
            string returnValue;
            Assembly asm = Assembly.GetExecutingAssembly();
            using (Stream rsrcStream = asm.GetManifestResourceStream(asm.GetName().Name + ".Properties." + strResourceName))
            {
                using (StreamReader sRdr = new StreamReader(rsrcStream))
                {
                    //For instance, gets it as text
                    returnValue = sRdr.ReadToEnd();
                }
            }
            return returnValue;
        }
        private static void Write(this string strResourceName, string value)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            using (Stream rsrcStream = asm.GetManifestResourceStream(asm.GetName().Name + ".Properties." + strResourceName))
            {
                using (StreamWriter sWdr = new StreamWriter(rsrcStream))
                {
                    //For instance, write
                    sWdr.Write(value);
                }
            }
        }

        public static void SaveDetails(this List<Details> details) => DetailsFile.Write(JsonConvert.SerializeObject(details));
        public static void SaveStat(this Stats stats) => StatFile.Write(JsonConvert.SerializeObject(stats));
        public static List<Details> ReadDetails() => JsonConvert.DeserializeObject<List<Details>>(Read(DetailsFile));
        public static Stats ReadStats() => JsonConvert.DeserializeObject<Stats>(Read(StatFile));


        public static Details InfosduJour(this List<Details> details) => details.FirstOrDefault();

        public static string Info(this Details details) => $"Nouvelle {details.Stat.ToString()}";

    }

}
