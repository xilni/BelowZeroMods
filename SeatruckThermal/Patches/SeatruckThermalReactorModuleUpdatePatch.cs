using HarmonyLib;
using UnityEngine;
using System;

namespace SeatruckThermal.Patches
{
    [HarmonyPatch(typeof(SeaTruckMotor), "Update")]
    class SeatruckThermalReactorModuleUpdatePatch
    {
        static float getThermalVal(float x)
        {
            return Mathf.Clamp((float)(x * 0.0342296 - 1.154310), 0, 1);
        }

        static void Prefix(SeaTruckMotor __instance)
        {
            if (__instance.upgrades.modules.GetCount(SeatruckThermal.seatruckThermalModule.TechType) > 0)
            {
                var waterSim = WaterTemperatureSimulation.main;
                var temperature = waterSim.GetTemperature(__instance.transform.position);
                var num = getThermalVal(temperature);
                __instance.relay.AddEnergy(num * Time.deltaTime, out float amountStored);
            }
        }
    }
}
