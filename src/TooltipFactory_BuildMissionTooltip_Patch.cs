using HarmonyLib;
using MGSC;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MGSC.TooltipProperty;

namespace QM_ShowTechLevel
{

    [HarmonyPatch(typeof(TooltipFactory), nameof(TooltipFactory.BuildMissionTooltip))]
    internal static class TooltipFactory_BuildMissionTooltip_Patch
    {

        public static void Postfix(TooltipFactory __instance, Mission mission)
        {


            if (mission.IsStoryMission || mission.ProcMissionType == ProceduralMissionType.CEOElimination)
            {
                return;
            }


            Faction faction = Plugin.State.Get<Factions>().Get(mission.BeneficiaryFactionId);
            __instance.AddPanelToTooltip().LocalizeName("tooltip.Power").SetValue($"{(faction.Power / 1000f):0.#}K");
            __instance.AddPanelToTooltip().LocalizeName("tooltip.TechLevel").SetValue(faction.CurrentTechLevel);
        }

    }
}
