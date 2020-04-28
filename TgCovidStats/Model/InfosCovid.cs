using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TgCovidStats.Model
{
    public class InfosCovid
    {
        public Stats InfosduJour { get { return Details.FirstOrDefault().Stat; } }
        public List<Details> Details { get; set; }
        public InfosCovid(List<Details> infos)
        {
            Details = infos;
        }
    }
}
