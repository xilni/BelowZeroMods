using UnityEngine;

namespace SeatruckThermal.Behaviours
{
    class SeatruckThermalBehaviour : MonoBehaviour
    {
        public SeaTruckUpgrades seatruck;

        // Values derived from a linear regression on the Exosuit Thermal Reactor and implemented here
        // so I don't have to copy over all the associated code that only lives there.
        static float getThermalVal(float x)
        {
            return Mathf.Clamp((float)(x * 0.0342296 - 1.154310), 0, 1);
        }

        public void Update()
        {
            var waterSim = WaterTemperatureSimulation.main;
            var temperature = waterSim.GetTemperature(seatruck.transform.position);
            var num = getThermalVal(temperature);
            seatruck.relay.AddEnergy(num * Time.deltaTime, out float amountStored);
        }
    }
}
