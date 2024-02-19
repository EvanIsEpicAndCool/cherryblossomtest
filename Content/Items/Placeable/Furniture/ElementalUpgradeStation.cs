using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace cherryblossomtest.Content.Items.Placeable.Furniture
{
    public class ElementalUpgradeStation : ModItem
    {
        // public override string Texture => $"Terraria/Images/Item_{ItemID.WorkBench}";

        public override void SetDefaults() {
			// ModContent.TileType<Tiles.Furniture.ExampleWorkbench>() retrieves the id of the tile that this item should place when used.
			// DefaultToPlaceableTile handles setting various Item values that placeable items use
			// Hover over DefaultToPlaceableTile in Visual Studio to read the documentation!
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.ElementalUpgradeStation>());
			Item.width = 28; // The item texture's width
			Item.height = 14; // The item texture's height
			Item.value = 150;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 10)
                .AddTile(TileID.WorkBenches)
				.Register();
		}
    }
}