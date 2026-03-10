using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Options.Models
{
    [Serializable]
    public class ScreenSelection
    {
       public List<AllVisibleScreens> AllVisibleScreensList { get; set; }
       public List<SelectedScreens_Mobile> SelectedScreensList_Mobile { get; set; }
      
       public List<SelectedScreens_Desktop> SelectedScreensList_Desktop { get; set; }
    

    }

    [Serializable]
    public class AllVisibleScreens
    {
        public string DisplayName { get; set; }
        public string ScreenID { get; set; }

    }
    [Serializable]
    public class SelectedScreens_Mobile
    {
        public string DisplayName { get; set; }
        public string ScreenID { get; set; }

    }

    [Serializable]
    public class SelectedScreens_Desktop
    {
        public string DisplayName { get; set; }
        public string ScreenID { get; set; }

    }

}