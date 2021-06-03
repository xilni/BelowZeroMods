using HarmonyLib;
using SeatruckSolar.Behaviours;
using UnityEngine;

namespace SeatruckSolar.Patches
{
    [HarmonyPatch]
    internal class SeaTruckUpgrades_OnUpgradeModuleChange_Patch
    {
        [HarmonyPatch(typeof(SeaTruckUpgrades), nameof(SeaTruckUpgrades.OnUpgradeModuleChange))]
        [HarmonyPostfix]
        public static void Postfix(SeaTruckUpgrades __instance, int slotID, TechType techType, bool added)
        {
            SeatruckSolarBehaviour solarModule = null;
            var noneEquipped = false;
            var foundBehaviour = false;

            // We only care about the Seatruck Solar Charger, return otherwise
            if (techType != SeatruckSolar.seatruckSolarModule.TechType)
            {
                return;
            }

            noneEquipped = __instance.modules.GetCount(techType) < 1;
            foundBehaviour = __instance.gameObject.TryGetComponent(out solarModule);

            // If equipped, proceed
            if (added && !foundBehaviour)
            {
                solarModule = __instance.gameObject.AddComponent<SeatruckSolarBehaviour>();
                solarModule.seatruck = __instance;
            }

            if (!added && noneEquipped && foundBehaviour)
                Object.Destroy(solarModule);
        }
    }
}
