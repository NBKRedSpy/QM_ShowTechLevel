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

        public static Mission Mission { get; set; }


        public static void Prefix(Mission mission)
        {
            ReplaceText = true;
            Mission = mission;
        }

        public static void Postfix()
        {
            ReplaceText = false;
            Mission = null;
        }
    }
}
