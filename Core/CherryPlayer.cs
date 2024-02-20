using cherryblossomtest.Core.Loaders.UILoading;
using cherryblossomtest.Core.Systems.CameraSystem;
using cherryblossomtest.Helpers;
using ReLogic.OS.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ID;
using static System.Net.Mime.MediaTypeNames;

namespace cherryblossomtest.Core
{
	public partial class CherryPlayer : ModPlayer
	{
		public bool IntroCutsceneStarted = false;
		public bool IntroCutsceneDone = false;

        public bool justHit = false;
		public int lastHit = 0;
		public bool trueInvisible = false;

		public int platformTimer = 0;

		public int pickupTimer = 0; //TODO: Move this into its own thing eventually
		public int maxPickupTimer = 0;
		public NPC pickupTarget;
		public Vector2 oldPickupPos;

		public bool inTutorial;

		public float itemSpeed;
		public float rotation;

		public static List<PlayerTicker> spawners = new();

		protected bool shouldSendHitPacket = false;

		public int Timer { get; private set; }

		public override void PreUpdate()
		{
			if (pickupTarget != null)
			{
				if (pickupTimer == 0)
					oldPickupPos = Player.Center;

				pickupTimer++;

				Player.immune = true;
				Player.immuneTime = 5;
				Player.immuneNoBlink = true;

				Player.Center = Vector2.SmoothStep(oldPickupPos, pickupTarget.Center, pickupTimer / 30f);
				if (pickupTimer >= maxPickupTimer)
					pickupTarget = null;
			}
			else
			{
				pickupTimer = 0;
			}

			platformTimer--;

			foreach (PlayerTicker ticker in spawners.Where(n => n.Active(Player) && Timer % n.TickFrequency == 0))
				ticker.Tick(Player);
		}

		public delegate void ResetEffectsDelegate(CherryPlayer Player);
		public static event ResetEffectsDelegate ResetEffectsEvent;
		public override void ResetEffects()
		{
			ResetEffectsEvent?.Invoke(this);
			itemSpeed = 1;

			trueInvisible = false;
			shouldSendHitPacket = false;
		}

		public override void PostUpdate()
		{

			if (Main.netMode == NetmodeID.MultiplayerClient && Player == Main.LocalPlayer)
				CherryWorld.visualTimer += (float)Math.PI / 60;
			Timer++;
		}

		public override void OnHurt(Player.HurtInfo info)
		{
			justHit = true;
			lastHit = Timer;
		}



		public delegate void PostUpdateEquipsDelegate(CherryPlayer Player);
		public static event PostUpdateEquipsDelegate PostUpdateEquipsEvent;
		public override void PostUpdateEquips()
		{
			PostUpdateEquipsEvent?.Invoke(this);
			justHit = false;
		}

		public override void OnEnterWorld()
		{
			
		}

		public override void OnRespawn()
		{
			if (Player == Main.LocalPlayer)
				CameraSystem.Reset();

			rotation = 0;
			inTutorial = false;
		}

		public override void PlayerConnect()
		{
			
		}

        public override float UseTimeMultiplier(Item Item)
		{
			return itemSpeed;
		}

		public void DoubleTapEffects(int keyDir)
		{
			
		}
	}
}