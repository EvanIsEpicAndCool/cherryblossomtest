﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace cherryblossomtest.Content
{
    public class CherryBlossomTreeLeaf : ModGore
    {
        public override string Texture => "cherryblossomtest/Content/CherryBlossomTreeLeaf";

        public override void SetStaticDefaults()
        {
            ChildSafety.SafeGore[Type] = true; // Leaf gore should appear regardless of the "Blood and Gore" setting
            GoreID.Sets.SpecialAI[Type] = 3; // Falling leaf behavior
            GoreID.Sets.PaintedFallingLeaf[Type] = true; // This is used for all vanilla tree leaves, related to the bigger spritesheet for tile paints
        }
    }
}
