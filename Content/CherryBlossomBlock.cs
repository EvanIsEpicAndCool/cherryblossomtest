using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace cherryblossomtest.Content
{
    public class CherryBlossomBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            TileObjectData.addTile(Type);
            AddMapEntry(new Microsoft.Xna.Framework.Color(200, 200, 200));
        }
    }
}
