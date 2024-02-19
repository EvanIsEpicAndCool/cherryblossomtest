using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using cherryblossomtest.Content.ElementDamageClasses;
using cherryblossomtest.Content.NPCS;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static cherryblossomtest.Content.ElementDamageClasses.ElementEnumberables;
using static cherryblossomtest.Content.NPCS.ModifyGlobalNPC;

namespace cherryblossomtest.Content.Items.Weapons
{
    public class FireWeapon : ModItem
    {
        int holdDustEffectTimer = 0;
        int holdDustEffectTimerLimit = 2;

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.knockBack = 6;
            Item.DamageType = ModContent.GetInstance<FireDamageClass>();
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            //This is how you can use GetModifierInheritance to add a modifier bonus to this weapon. I think this might be useful for upgrading the elemental weapons later on
            //however this method is wack af so might take some messing around with (I don't really know what it does)
            //StatInheritanceData data = Item.DamageType.GetModifierInheritance(ModContent.GetInstance<FireDamageClass>());
            //Item.useTime /= (int)data.attackSpeedInheritance;
            //Item.useAnimation /= (int)data.attackSpeedInheritance;
        }

        // Because we want the damage tooltip to show our custom damage, we need to modify it
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Get the vanilla damage tooltip
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
            if (tt != null)
            {
                // We want to grab the last word of the tooltip, which is the translated word for 'damage' (depending on what language the player is using)
                // So we split the string by whitespace, and grab the last word from the returned arrays to get the damage word, and the first to get the damage shown in the tooltip
                string[] splitText = tt.Text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                // Change the tooltip text
                tt.Text = damageValue + " fire " + damageWord;
            }
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            for (int i = 0; i < 15; i++)
                Dust.NewDust(target.position, 50, 50, DustID.Blood, SpeedX: 0, SpeedY: -1, 1, default, 1.5f);
        
            ElementTypes elementType = target.GetGlobalNPC<ModifyGlobalNPC>().GetElementType();

            switch(elementType)
            {
                case ElementTypes.Nature:
                // This is how you can add a bonus against other elements
                    modifiers.FinalDamage *= 5;
                    target.AddBuff(BuffID.OnFire, 60 * 5, false);
                    break;
                default:
                    break;
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // for(int i = 0; i < 5; i++)
                int dustIndex = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), 50, 50, DustID.LavaMoss, SpeedX: 0, SpeedY: 0, 1, default, 0.8f);
                Dust dust = Main.dust[dustIndex];
                dust.velocity = new Vector2(0, 0);
                
        }

        public override void HoldItem(Player player)
        {
            holdDustEffectTimer++;

            if(holdDustEffectTimer >= holdDustEffectTimerLimit)
            {
                int dustId = Dust.NewDust(player.position - new Vector2(15, 0), player.width + 30, player.height, 55, SpeedX: 0, SpeedY: 0, 0, default, 0.75f);
                Main.dust[dustId].velocity = new Vector2(0, -0.3f);

                holdDustEffectTimer = 0;
            }            
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)
                .AddTile<Tiles.Furniture.ElementalUpgradeStation>()
                .Register();
        }
    }
}