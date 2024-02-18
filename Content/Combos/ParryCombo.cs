using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using cherryblossomtest.Core.Systems;
using Terraria.Graphics.Effects;

public class ParryCombo : ModPlayer
{

    ComboSystem comboSystem = ModContent.GetInstance<ComboSystem>();
    private bool canParry = true;
    private const int parryWindow = 20; // The frame window in which a parry is possible
    private int parryCooldownTimer = 0; // Cooldown timer to prevent spamming the parry

    // Parry combo key sequence
    private readonly string parryComboSequence = "Z-X-C";

    public override void Initialize()
    {
        // Register the parry combo with the ComboSystem
        comboSystem.RegisterCombo(parryComboSequence, PerformParry);
    }

    private void PerformParry()
    {
        // Check if the player can parry
        if (canParry && parryCooldownTimer == 0)
        {
            // Implement parry mechanic: invulnerability and a counter window
            Player.immune = true;
            Player.immuneTime = parryWindow;
            parryCooldownTimer = 60; // 1 second cooldown at 60 FPS

            // Trigger visual effects for parrying
            ApplyParryShader();
            CreateParryDust(Player.position);
            EmitParryParticles(Player.Center);

            // You could also trigger a sound effect here
            // Main.PlaySound(SoundID.Item, Player.position, <sound_effect_id>);
        }
    }

    public override void PostUpdate()
    {
        // Update the cooldown timer for parry
        if (parryCooldownTimer > 0)
        {
            parryCooldownTimer--;
            if (parryCooldownTimer == 0)
            {
                canParry = true; // Reset parry ability
            }
        }
    }

    private void ApplyParryShader()
    {
        // Apply a shader effect briefly to the player. Assumes you have a shader named "ParryEffect"
        Filters.Scene.Activate("ParryEffect", Player.Center).Deactivate(10f);
    }

    private void CreateParryDust(Vector2 position)
    {
        // Create dust effects at the player's position
        for (int i = 0; i < 20; i++)
        {
            Dust.NewDust(position, Player.width, Player.height, DustID.GoldFlame, Alpha: 150, Scale: 1.5f);
        }
    }

    private void EmitParryParticles(Vector2 position)
    {
        // Emit particles to illustrate the parry force. Placeholder for your particle system.
    }
}
