using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeatruckThermal.Modules
{
    class SeatruckThermalReactorModule : Equipable
    {
        public SeatruckThermalReactorModule() : base(
            "SeaTruckUpgradeThermalReactor",
            "Seatruck Thermal Reactor",
            "Recharges power cells in hot areas (doesn't stack).")
        {}

        public override EquipmentType EquipmentType => EquipmentType.SeaTruckModule;
        public override TechType RequiredForUnlock => TechType.ExosuitThermalReactorModule;
        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;
        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
        public override CraftTree.Type FabricatorType => CraftTree.Type.SeamothUpgrades;
        public override string[] StepsToFabricatorTab => new string[] { "SeaTruckUpgrade" };
        public override QuickSlotType QuickSlotType => QuickSlotType.Passive;

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.ExosuitThermalReactorModule);
            yield return task;
            GameObject originalPrefab = task.GetResult();
            GameObject resultPrefab = GameObject.Instantiate(originalPrefab);
            gameObject.Set(resultPrefab);
        }

        // Same recipe as Prawn Thermal Reactor Module
        protected override RecipeData GetBlueprintRecipe()
        {
            return new RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(TechType.Kyanite, 2),
                    new Ingredient(TechType.Polyaniline, 2),
                    new Ingredient(TechType.WiringKit, 1)
                }
            };
        }

        protected override Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.ExosuitThermalReactorModule);
        }
    }
}
