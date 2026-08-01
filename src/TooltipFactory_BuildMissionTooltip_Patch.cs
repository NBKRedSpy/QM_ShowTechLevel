using HarmonyLib;
using MGSC;
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

    [HarmonyPatch(typeof(TooltipFactory), nameof(TooltipFactory.BuildMissionTooltip))]
    internal static class TooltipFactory_BuildMissionTooltip_Patch
    {

        public static void Postfix(TooltipFactory __instance, Mission mission)
        {
            //Hack to avoid a transpile.
            //  The existing tooltip lines are in the _usedPropertyPanels list.  Since there is no text, match on the icon.
            SetTechLevelText(__instance._usedPropertyPanels, "common_beneficiary", mission.BeneficiaryFactionId);
            SetTechLevelText(__instance._usedPropertyPanels, "common_panic", mission.VictimFactionId);
        }

        private static void SetTechLevelText(List<TooltipProperty> usedPropertyPanels, string spriteId, string factionId)
        {
            TooltipProperty property = null;

            property = usedPropertyPanels
                .Where(x => x.Icon.sprite == Data.TooltipIcons.GetSpriteByTag(spriteId)).FirstOrDefault();

            if (property == null) return;

            Faction faction = Plugin.State.Get<Factions>().Get(factionId);

            if (faction == null) return;

            string text = property.Value.text;
            string newText = $"<size=70%>{faction.CurrentTechLevel} / {(faction.Power / 1000f):0.#}K</size> {text}";

            property.SetValue(newText, false);

        }

    }
}
