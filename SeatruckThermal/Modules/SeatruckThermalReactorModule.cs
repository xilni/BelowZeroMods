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
            "SeatruckThermalReactor",
            "Seatruck Thermal Reactor",
            "Recharges power cells in hot areas (doesn't stack).")
        {
            OnFinishedPatching += () =>
            {
                SMLHelper.V2.Handlers.CraftTreeHandler.Main.AddCraftingNode(CraftTree.Type.Fabricator, this.TechType, new string[] { "Upgrades", "SeatruckUpgrades" });
                SMLHelper.V2.Handlers.CraftTreeHandler.Main.AddCraftingNode(CraftTree.Type.SeaTruckFabricator, this.TechType, new string[] { "Upgrades" });
                SMLHelper.V2.Handlers.CraftTreeHandler.Main.AddCraftingNode(CraftTree.Type.SeamothUpgrades, this.TechType, new string[] { "SeaTruckUpgrade" });
                SMLHelper.V2.Handlers.CraftTreeHandler.Main.AddCraftingNode(CraftTree.Type.Workbench, this.TechType, new string[] { "SeaTruckWBUpgrades" });
            };
        }

        public override EquipmentType EquipmentType => EquipmentType.SeaTruckModule;
        public override TechType RequiredForUnlock => TechType.ExosuitThermalReactorModule;
        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;
        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
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
