using HarmonyLib;
using UnityEngine;

namespace SeatruckSolar.Patches
{
    [HarmonyPatch(typeof(SeaTruckMotor), "Update")]
    class SeatruckSolarChargerModuleUpdatePatch
    {
        // Constants
        private const float maxSolarDepth = 200f;

        static void Prefix(SeaTruckMotor __instance)
        {
            var moduleCount = __instance.upgrades.modules.GetCount(SeatruckSolar.seatruckSolarModule.TechType);

            // If equipped, proceed
            if (moduleCount > 0)
            {
                // Determine light value
                DayNightCycle main = DayNightCycle.main;
                float depthScalar = Mathf.Clamp01((maxSolarDepth + __instance.transform.position.y) / maxSolarDepth);
                float localLightScalar = main.GetLocalLightScalar();

                // Add energy to vehicle
                float amount = localLightScalar * depthScalar * (float)moduleCount;
                __instance.relay.AddEnergy(amount * Time.deltaTime, out float amountStored);
            }
        }
    }
}
