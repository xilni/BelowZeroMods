using UnityEngine;

namespace SeatruckSolar.Behaviours
{
    class SeatruckSolarBehaviour : MonoBehaviour
    {
        public SeaTruckUpgrades seatruck;
        private const float maxSolarDepth = 200;

        public void UpdateSolarRecharge()
        {
            // Determine light value
            DayNightCycle main = DayNightCycle.main;
            float depthScalar = Mathf.Clamp01((maxSolarDepth + seatruck.transform.position.y) / maxSolarDepth);
            float localLightScalar = main.GetLocalLightScalar();

            // Add energy to vehicle
            float amount = localLightScalar * depthScalar;
            seatruck.relay.AddEnergy(amount, out float amountStored);
        }

        public void Start()
        {
            InvokeRepeating("UpdateSolarRecharge", 1f, 1f);
        }
    }
}
