using HarmonyLib;
using MGSC;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static MGSC.TooltipProperty;

namespace ShowTechLevel
{

    //TODO:  Rename the class after testing.

    [HarmonyPatch(typeof(TooltipFactionHeader), nameof(TooltipFactionHeader.Initialize))]
    internal static class TooltipFactory_BuildMissionTooltip_Patch
    {



        public static void Postfix(TooltipFactionHeader __instance, Faction faction)
        {
            string techLevel = __instance._techLevelDesc.text;
            __instance._techLevelDesc.SetText($"<size=60%>{techLevel}/{(faction.Power / 1000f):0.#}k</size>");

        }
    }
}
