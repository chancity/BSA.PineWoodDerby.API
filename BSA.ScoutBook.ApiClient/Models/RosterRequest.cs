using System.Collections.Generic;

namespace BSA.ScoutBook.ApiClient.Models
{
    public class RosterRequest
    {
        public int UnitID { get; set; }
        public bool ShowScouts { get; set; }
        public bool ShowDenPatrol { get; set; }
        public string Title { get; set; }

        public RosterRequest(int unitId, bool showScouts, bool showDenPatrol, string title = null)
        {
            UnitID = unitId;
            ShowScouts = showScouts;
            ShowDenPatrol = showDenPatrol;
            Title = title;
        }

        public Dictionary<string, object> ToDictionary() => new Dictionary<string, object>()
        {
            {"UnitID",UnitID },
            {"ShowScouts",ShowScouts ? 1 : 0 },
            {"ShowDenPatrol", ShowDenPatrol ? 1 : 0 },
            {"Title","" },
        };
    }


}
