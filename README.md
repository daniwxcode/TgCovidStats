# TgCovidStats
Package Nuget permettant de récupérer les informations sur la situation de la maladie au Corona Virus au Togo
Ce Package nuget a pour source de données le site Officiel du Gouvernement Togolais. : 
## https://covid19.gouv.tg/ 


# Les Objets retournés par le Package : 
## les Statistiques 
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
    
### Comment les reccupérer?

       public async Task<TgCovidStats.Model.Stats> GetAsync()=>await TgCovidStats.Get.StatAsync();
       
    Ce code vous permet de reccuperer les dernières statistiques du Pays
    
 ## Le Detail des informations 
 Il s'agit essentiellement de reccupérer toutes les hisoires affiché sur la page 
 ## https://covid19.gouv.tg/situation-au-togo/
 les model sont : 
   public class Details
    {
        public Stats Stat { get; set; }
        public string Date { get; set; }
        public string Histoire { get; set; }
    }
   public class InfosCovid
    {
        public Stats InfosduJour { get { return Details.FirstOrDefault().Stat; } }
        public List<Details> Details { get; set; }
        public InfosCovid(List<Details> infos)
        {
            Details = infos;
        }
    }  
    
 ### Comment les reccupérer?
       public async Task<TgCovidStats.Model.InfosCovid> GetDetailsAsync()=>await TgCovidStats.Get.DetailsAsync();



