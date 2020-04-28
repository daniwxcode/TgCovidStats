# TgCovidStats
Package Nuget permettant de récupérer les informations sur la situation de la maladie au Corona Virus au Togo
Ce Package nuget a pour source de données le site Officiel du Gouvernement Togolais. : 
## https://covid19.gouv.tg/ 

# Les Objets retournés par le Package : 
## les Statisituqes 
    public class Stats
    {
        public uint ActiveCases { get; set; }
        public uint Cured { get; set; }
        public uint Deaths { get; set; }
        public uint Total { get { return ActiveCases + Cured + Deaths; } }

        public string TimeInfo { get; set; }

        public override string ToString()
        {
            return $"\n Actifs= {ActiveCases} -Guéris={Cured} - Décès ={Deaths} - Total ={Total}";
        }
    }



