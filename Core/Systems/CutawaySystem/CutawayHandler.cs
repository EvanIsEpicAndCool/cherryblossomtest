using Terraria.DataStructures;

namespace cherryblossomtest.Core.Systems.CutawaySystem
{
	internal class CutawayHandler : ModSystem
	{
		public static bool created = false;

		public static void CreateCutaways()
		{
			CutawayHook.cutaways.Clear();
		}

		public override void PostUpdateEverything()
		{
			if (!Main.dedServ && !created)
			{
				CreateCutaways();
				created = true;
			}
		}

		public override void OnWorldUnload()
		{
			created = false;
		}
	}
}