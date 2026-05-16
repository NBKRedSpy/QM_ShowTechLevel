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


            //Hack to reset the text.

            //__instance.AddPanelToTooltip().LocalizeName("tooltip.Power").SetValue($"{(faction.Power / 1000f):0.#}K");
            //__instance.AddPanelToTooltip().LocalizeName("tooltip.TechLevel").SetValue(faction.CurrentTechLevel);


            SetTechLevelText(__instance._usedPropertyPanels, "common_beneficiary", mission.BeneficiaryFactionId);

            SetTechLevelText(__instance._usedPropertyPanels, "common_panic", mission.VictimFactionId);



            //TooltipProperty property = null;

            //property = __instance._usedPropertyPanels
            //    .Where(x => x.Icon.sprite == Data.TooltipIcons.GetSpriteByTag("common_beneficiary")).FirstOrDefault();

            //if(property != null)
            //{
            //    Faction faction = Plugin.State.Get<Factions>().Get(mission.BeneficiaryFactionId);

            //    string text = property.Value.text;
            //    string newText = $"<size=70%>{faction.CurrentTechLevel} / {(faction.Power/1000f):0.#}K</size> {text}";

            //    property.SetValue(newText, false);
            //}

            ////__instance._usedPropertyPanels[0].Icon.sprite  == Data.TooltipIcons.GetSpriteByTag("common_beneficiary")
        }

        private static void SetTechLevelText(List<TooltipProperty> usedPropertyPanels, string spriteId, string factionId)
        {
            TooltipProperty property = null;

            property = usedPropertyPanels
                .Where(x => x.Icon.sprite == Data.TooltipIcons.GetSpriteByTag(spriteId)).FirstOrDefault();

            if (property == null) return;

            Faction faction = Plugin.State.Get<Factions>().Get(factionId);

            string text = property.Value.text;
            string newText = $"<size=70%>{faction.CurrentTechLevel} / {(faction.Power / 1000f):0.#}K</size> {text}";

            property.SetValue(newText, false);

        }

    }
}
