using HarmonyLib;
using QModManager.API.ModLoading;

namespace SeatruckThermal
{
    [QModCore]
    public class SeatruckThermal
    {

        internal static Modules.SeatruckThermalReactorModule seatruckThermalModule = new Modules.SeatruckThermalReactorModule();

        [QModPatch]
        public static void Load()
        {
            seatruckThermalModule.Patch();

            Harmony harmony = new Harmony("com.xilni.seatruckthermal");
            harmony.PatchAll();
        }
    }
}
