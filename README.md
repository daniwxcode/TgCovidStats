# TgCovidStats
Package Nuget permettant de récupérer les informations sur la situation de la maladie au Corona Virus au Togo
Ce Package nuget a pour source de données le site Officiel du Gouvernement Togolais. : 
## https://covid19.gouv.tg/ 


# Les Objets retournés par le Package : 
## les Statistiques 
    
### Comment les reccupérer?

       public async Task<TgCovidStats.Model.Stats> GetAsync()=>await TgCovidStats.Get.StatAsync();
       
    Ce code vous permet de reccuperer les dernières statistiques du Pays
    
 ## Le Detail des informations 
 Il s'agit essentiellement de reccupérer toutes les hisoires affiché sur la page 
 ## https://covid19.gouv.tg/situation-au-togo/

 ### Comment les reccupérer?
       public async Task<TgCovidStats.Model.InfosCovid> GetDetailsAsync()=>await TgCovidStats.Get.DetailsAsync();



