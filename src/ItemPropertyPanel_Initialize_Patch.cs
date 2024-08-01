using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGSC.ItemPropertyPanel;

namespace QM_ShowTechLevel
{

    [HarmonyPatch(typeof(ItemPropertyPanel), nameof(ItemPropertyPanel.Initialize),
        new Type[] { typeof(ItemPropertyType), typeof(string), typeof(ComprasionType), typeof(string) })]
    internal static class ItemPropertyPanel_Initialize_Patch
    {
        public static Factions Factions { get; set; }


        public static void Prefix(ItemPropertyType propertyType, ref string value)
        {
            if (!TooltipFactory_BuildStationTooltip_Patch.ReplaceText) return;

            string factionId;
            switch (propertyType)
            {
                case ItemPropertyType.StationOwner:
                    factionId = TooltipFactory_BuildStationTooltip_Patch.Station.OwnerFactionId;
                    break;
                case ItemPropertyType.MissionCustomer:
                    if (TooltipFactory_BuildStationTooltip_Patch.StationMission == null) return;

                    factionId = TooltipFactory_BuildStationTooltip_Patch.StationMission.BeneficiaryFactionId;
                    break;
                case ItemPropertyType.Attackers:
                    if (TooltipFactory_BuildStationTooltip_Patch.StationMission == null) return;

                    factionId = TooltipFactory_BuildStationTooltip_Patch.StationMission.VictimFactionId;
                    break;
                default:
                    return;
            }

            if (Factions == null)
            {
                Factions = Plugin.State.Get<Factions>();
            }

            Faction faction = Factions.Get(factionId);

            value = $"<size=70%>({faction.TechLevel:0.###})</size> {value}";
        }

    }
}
