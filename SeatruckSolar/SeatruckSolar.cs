using HarmonyLib;
using QModManager.API.ModLoading;

namespace SeatruckSolar
{
    [QModCore]
    public class SeatruckSolar
    {
        internal static Modules.SeatruckSolarChargerModule seatruckSolarModule = new Modules.SeatruckSolarChargerModule();

        [QModPatch]
        public static void Load()
        {
            seatruckSolarModule.Patch();

            Harmony harmony = new Harmony("com.xilni.seatrucksolar");
            harmony.PatchAll();
        }
    }
}
