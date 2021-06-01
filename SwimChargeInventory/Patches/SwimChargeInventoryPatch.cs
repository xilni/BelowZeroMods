using HarmonyLib;
using UnityEngine;

namespace SwimChargeInventory.Patches
{
    [HarmonyPatch(typeof(UpdateSwimCharge), "FixedUpdate")]
    class SwimChargeInventoryPatch
    {
        static void Prefix(UpdateSwimCharge __instance)
        {
            // If not underwater or moving quick enough, return early
            if (!Player.main.IsUnderwater() || Player.main.gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 2f)
            {
                return;
            }

            // If no swim fins equipped, return early
            if (Inventory.Get().equipment.GetCount(TechType.SwimChargeFins) < 1)
            {
                return;
            }

            // If held tool needs charge, return early and let unpatched method handle it
            if (HeldToolNeedsCharge())
            {
                return;
            }

            // Check config to charge batteries
            var chargeBatteries = SwimChargeInventory.config.chargeBatteries;

            // Iterate through inventory looking for chargeables
            foreach (var item in Inventory.Get().container)
            {
                // Check if item is chargeable
                EnergyMixin energy_component = item.item.GetComponent<EnergyMixin>();
                if (energy_component != null)
                {
                    // Does item even have a battery?
                    var battery = energy_component.GetBattery();
                    if (battery != null)
                    {
                        // Only try to charge if charge less than 100%
                        if (battery.charge < battery.capacity)
                        {
                            // Add charge
                            energy_component.AddEnergy(__instance.chargePerSecond * Time.deltaTime);
                            break;
                        }
                    }
                }

                // If we're charging batteries
                if (chargeBatteries &&
                    item.item.TryGetComponent<IBattery>(out IBattery ibatteryComponent) &&
                    ibatteryComponent.charge < ibatteryComponent.capacity)
                {
                    // Add some charge
                    ibatteryComponent.charge += __instance.chargePerSecond * Time.deltaTime;
                    break;
                }
            }
        }

        private static bool HeldToolNeedsCharge()
        {
            var heldTool = Inventory.main.GetHeldTool();
            var heldToolNeedsCharge = false;

            // If held item
            if (heldTool != null)
            {
                // Is held tool chargeable and below 100% charge?
                if (heldTool.gameObject.TryGetComponent(out EnergyMixin heldToolEnergyMixin) && (heldToolEnergyMixin.charge < heldToolEnergyMixin.capacity))
                {
                    heldToolNeedsCharge = true;
                }
            }

            return heldToolNeedsCharge;
        }
    }
}
