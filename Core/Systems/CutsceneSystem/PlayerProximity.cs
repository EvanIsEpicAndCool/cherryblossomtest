using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace cherryblossomtest.Core.Systems.CutsceneSystem
{
    internal class PlayerProximity : ModNPC
    {
        public SpriteFont Font;
        private bool allPlayersInRange = false;
        private float radius = 100f;
        private int playersInProximityCount = 0;

        public override void AI()
        {
            playersInProximityCount = CountPlayersInRange();
            allPlayersInRange = playersInProximityCount == Main.player.Count(p => p.active && !p.dead);
        }

        private void CreateDustEffect()
        {
                for (int i = 0; i < 360; i += 10) // Adjust the step for more/less dust
                {
                    Vector2 dustPosition = NPC.Center + Vector2.One.RotatedBy(MathHelper.ToRadians(i)) * radius;
                    Dust.NewDustPerfect(dustPosition, 10, Vector2.Zero).noGravity = true;
                }
        }

        private bool CheckPlayersInRange()
        {
            foreach (Player player in Main.player)
            {
                // Check if player is active and not too far from the NPC
                if (player.active && !player.dead && Vector2.Distance(player.Center, NPC.Center) > radius)
                {
                    return false;
                }
            }
            return true; // All players are in range
        }

        private int CountPlayersInRange()
        {
            int count = 0;
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead && Vector2.Distance(player.Center, NPC.Center) <= radius)
                {
                    count++;
                }
            }
            return count;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            string text = $"{playersInProximityCount}/{Main.player.Count(p => p.active && !p.dead)}";
            Vector2 textPosition = NPC.Center / 2f + new Vector2(0f, -radius - 20f); // Adjust as needed
            spriteBatch.DrawString(Font, text, textPosition, Color.White);
        }
    }


}
