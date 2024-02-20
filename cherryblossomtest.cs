global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using cherryblossomtest.Core;
global using Terraria;
global using Terraria.Localization;
global using Terraria.ModLoader;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using ReLogic.Content;



namespace cherryblossomtest
{
	public class cherryblossomtest : Mod
	{

        public static cherryblossomtest Instance { get; set; }

        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> filterRef = new Ref<Effect>(this.Assets.Request<Effect>("Content/Shaders/ParryShader", AssetRequestMode.ImmediateLoad).Value);
                Filters.Scene["ParryEffect"] = new Filter(new ScreenShaderData(filterRef, "Parry"), EffectPriority.Medium);
            }
                
        }
        public override void PostSetupContent()
        {
            NetEasy.NetEasy.Register(this);
        }
    }
}