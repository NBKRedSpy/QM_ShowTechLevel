using HarmonyLib;
using MGSC;
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

    [HarmonyPatch(typeof(TooltipProperty), nameof(TooltipProperty.Initialize),
        new Type[] { typeof(ItemPropertyType), typeof(string), typeof(ComprasionType), typeof(string) })]
    internal static class ItemPropertyPanel_Initialize_Patch
    {
        public static Factions Factions { get; set; }


        public static void Prefix(ItemPropertyType propertyType, ref string value)
        {
            if (!TooltipFactory_BuildStationTooltip_Patch.ReplaceText ||
                (propertyType != ItemPropertyType.Beneficiary && propertyType != ItemPropertyType.Victim))
            {
                return;
            }

            Mission mission = TooltipFactory_BuildStationTooltip_Patch.Mission;

            string factionId = propertyType == ItemPropertyType.Beneficiary ?
                mission.BeneficiaryFactionId : mission.VictimFactionId;

            if (string.IsNullOrEmpty(factionId)) return;

            if (Factions == null)
            {
                Factions = Plugin.State.Get<Factions>();
            }

            Faction faction = Factions.Get(factionId);

            value = $"<size=70%>{faction.CurrentTechLevel} / {(faction.Power/1000f):0.#}K</size> {value}";
        }

    }
}
