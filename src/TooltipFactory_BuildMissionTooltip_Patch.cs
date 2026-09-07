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
    [HarmonyPatch(typeof(TooltipFactionHeader), nameof(TooltipFactionHeader.Initialize))]
    internal static class TooltipFactionHeader_Initialize_Patch
    {
        public static void Postfix(TooltipFactionHeader __instance, Faction faction)
        {

            __instance._techLevelDesc.SetText($"<size=70%>{faction.CurrentTechLevel} - {(faction.Power / 1000f):0.#}k</size>");

        }
    }
}
