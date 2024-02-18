using Terraria.ModLoader;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;


namespace cherryblossomtest
{
	public class cherryblossomtest : Mod
	{

        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> filterRef = new Ref<Effect>(this.Assets.Request<Effect>("Content/Shaders/ParryShader", AssetRequestMode.ImmediateLoad).Value);
                Filters.Scene["ParryEffect"] = new Filter(new ScreenShaderData(filterRef, "Parry"), EffectPriority.Medium);
            }
                
        }
    }
}