using cherryblossomtest.Content.GUI;
using System;
using System.IO;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace cherryblossomtest.Core
{
	public partial class CherryWorld : ModSystem
	{
		public static CherryWorld worldInstance;

		private static WorldFlags flags;

		public static float visualTimer;

		public static Rectangle vitricBiome = new();

		public static Rectangle squidBossArena = new();

		public static Rectangle VitricBossArena => new(vitricBiome.X + vitricBiome.Width / 2 - 56, vitricBiome.Y - 1, 108, 74); //ceiros arena

		private static Vector2 GlassweaverArenaPos => vitricBiome.TopLeft() * 16 + new Vector2(0, 80 * 16) + new Vector2(0, 256);
		public static Rectangle GlassweaverArena => new((int)GlassweaverArenaPos.X - 35 * 16, (int)GlassweaverArenaPos.Y - 30 * 16, 70 * 16, 30 * 16);

		public CherryWorld()
		{
			worldInstance = this;
		}

		public static bool HasFlag(WorldFlags flag)
		{
			return (flags & flag) != 0;
		}

		public static void Flag(WorldFlags flag)
		{
			flags |= flag;
			NetMessage.SendData(MessageID.WorldData);
		}

		public static void FlipFlag(WorldFlags flag)
		{
			flags ^= flag;
			NetMessage.SendData(MessageID.WorldData);
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write((int)flags);

			WriteRectangle(writer, vitricBiome);
			WriteRectangle(writer, squidBossArena);
		}

		public override void NetReceive(BinaryReader reader)
		{
			flags = (WorldFlags)reader.ReadInt32();

			vitricBiome = ReadRectangle(reader);
			squidBossArena = ReadRectangle(reader);
		}

		private void WriteRectangle(BinaryWriter writer, Rectangle rect)
		{
			writer.Write(rect.X);
			writer.Write(rect.Y);
			writer.Write(rect.Width);
			writer.Write(rect.Height);
		}

		private Rectangle ReadRectangle(BinaryReader reader)
		{
			return new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
		}

		public override void PreUpdateWorld()
		{
			visualTimer += (float)Math.PI / 60;

			if (visualTimer >= Math.PI * 2)
				visualTimer = 0;
		}

		public override void PostUpdateWorld()
		{
		}

		public override void ClearWorld()
		{
			vitricBiome.X = 0;
			vitricBiome.Y = 0;

			flags = default;
		}

		public override void OnWorldLoad()
		{
			if (!Main.dedServ)
				RichTextBox.CloseDialogue(); //Safeguard
		}

		public override void SaveWorldData(TagCompound tag)
		{

			tag[nameof(flags)] = (int)flags;
		}

		public override void LoadWorldData(TagCompound tag)
		{

			flags = (WorldFlags)tag.GetInt(nameof(flags));
		}

		public override void Unload()
		{
		}
	}
}