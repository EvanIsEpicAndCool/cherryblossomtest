using cherryblossomtest.Content.Tiles.Furniture;
using cherryblossomtest.Core.UI.UpgradeStationUI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace cherryblossomtest.Content.Tiles
{
    public class ModGlobalTile : GlobalTile
    {
        public ModGlobalTile instance;

        public ModGlobalTile()
        {
            instance = this;
        }

        public override void RightClick(int i, int j, int type)
        {
            if(type == ModContent.TileType<ElementalUpgradeStation>())
            {
                Main.NewText("awesome", 0, 255, 0);
                ModContent.GetInstance<ElementalUpgradeStationUISystem>().ShowMyUI();
            }
        }
    }
}