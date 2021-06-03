using HarmonyLib;
using SeatruckThermal.Behaviours;
using UnityEngine;

namespace SeatruckThermal.Patches
{
    [HarmonyPatch]
    internal class SeaTruckUpgrades_OnUpgradeModuleChange_Patch
    {
        [HarmonyPatch(typeof(SeaTruckUpgrades), nameof(SeaTruckUpgrades.OnUpgradeModuleChange))]
        [HarmonyPostfix]
        public static void Postfix(SeaTruckUpgrades __instance, int slotID, TechType techType, bool added)
        {
            SeatruckThermalBehaviour thermalModule = null;
            var noneEquipped = false;
            var foundBehaviour = false;

            // We only care about the Seatruck Thermal Reactor, return otherwise
            if (techType != SeatruckThermal.seatruckThermalModule.TechType)
            {
                return;
            }

            noneEquipped = __instance.modules.GetCount(techType) < 1;
            foundBehaviour = __instance.gameObject.TryGetComponent(out thermalModule);

            // If equipped, proceed
            if (added && !foundBehaviour)
            {
                thermalModule = __instance.gameObject.AddComponent<SeatruckThermalBehaviour>();
                thermalModule.seatruck = __instance;
            }

            if (!added && noneEquipped && foundBehaviour)
                Object.Destroy(thermalModule);
        }
    }
}
