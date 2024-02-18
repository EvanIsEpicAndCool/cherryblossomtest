using SubworldLibrary;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using Terraria.ModLoader;
using cherryblossomtest;

namespace cherryblossomtest
{
    public class CherryBlossomSubworld : Subworld
    {

        public override int Height => 223;
        public override int Width => 511;
        public override void Load()
        {
            Main.dayTime = true;
            Main.time = 27000.0;

            // Other subworld properties
        }

        public override List<GenPass> Tasks => new List<GenPass>()
    {
        new LoadWorldGenPass()

    };

        private void Decorating(GenerationProgress progress)
        {
            // Add decorations, like grass, flowers, etc.
        }

        public override void Update()
        {
            Main.dayTime = true;
            Main.time = 27000.0; // Keep it midday
        }


    }
    public class LoadWorldGenPass : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public LoadWorldGenPass() : base("Load", 100) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            WorldFile.LoadWorld_Version2(new BinaryReader(new MemoryStream(ModContent.GetInstance<cherryblossomtest>().GetFileBytes("CherryBlossomWorld.wld"))));
        }
    }
}