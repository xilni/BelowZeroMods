using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeatruckSolar.Modules
{
    class SeatruckSolarChargerModule : Equipable
    {
        public SeatruckSolarChargerModule() : base(
            "SeatruckSolarCharger",
            "Seatruck Solar Charger",
            "Recharges the Seatruck's power cells while in sunlight.")
        {
            OnFinishedPatching += () =>
            {
                SMLHelper.V2.Handlers.CraftTreeHandler.Main.AddCraftingNode(CraftTree.Type.Fabricator, this.TechType, new string[] { "Upgrades", "SeatruckUpgrades" });
                SMLHelper.V2.Handlers.CraftTreeHandler.Main.AddCraftingNode(CraftTree.Type.SeamothUpgrades, this.TechType, new string[] { "SeaTruckUpgrade" });
            };
        }

        public override EquipmentType EquipmentType => EquipmentType.SeaTruckModule;
        public override TechType RequiredForUnlock => TechType.SeaTruck;
        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;
        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
        public override QuickSlotType QuickSlotType => QuickSlotType.Passive;

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.SeamothSolarCharge);
            yield return task;
            GameObject originalPrefab = task.GetResult();
            GameObject resultPrefab = GameObject.Instantiate(originalPrefab);
            gameObject.Set(resultPrefab);
        }

        // Same recipe as old Seamoth Solar Charger
        protected override RecipeData GetBlueprintRecipe()
        {
            return new RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(TechType.AdvancedWiringKit, 1),
                    new Ingredient(TechType.EnameledGlass, 1)
                }
            };
        }

        protected override Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.SeamothSolarCharge);
        }
    }
}
