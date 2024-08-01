using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace QM_ShowTechLevel
{


    [HarmonyPatch(typeof(TooltipFactory), nameof(TooltipFactory.BuildStationTooltip))]
    public static class TooltipFactory_BuildStationTooltip_Patch
    {


        public static bool ReplaceText { get; set; }
        public static Station Station { get; set; }
        public static StationStatus StationStatus { get; set; }
        public static Mission StationMission { get; set; }


        public static void Prefix(Station station, StationStatus stationStatus, Mission stationMission)
        {
            ReplaceText = true;
            Station = station;
            StationStatus = stationStatus;
            StationMission = stationMission;
        }

        public static void Postfix()
        {
            ReplaceText = false;
            Station = null;
            StationMission = null;
        }
    }
}
