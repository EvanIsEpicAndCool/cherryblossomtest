using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace cherryblossomtest.Core.UI.UpgradeStationUI
{
	[Autoload(Side = ModSide.Client)] // This attribute makes this class only load on a particular side. Naturally this makes sense here since UI should only be a thing clientside. Be wary though that accessing this class serverside will error
	public class ElementalUpgradeStationUISystem : ModSystem
	{
		private UserInterface upgradeStationInterface;
		internal ElementalUpgradeStationUI upgradeStationUI;

		// These two methods will set the state of our custom UI, causing it to show or hide
		public void ShowMyUI() {
			upgradeStationInterface?.SetState(upgradeStationUI);
		}

		public void HideMyUI() {
			upgradeStationInterface?.SetState(null);
		}

		public override void Load() {
			// Create custom interface which can swap between different UIStates
			upgradeStationInterface = new UserInterface();
			// Creating custom UIState
			upgradeStationUI = new ElementalUpgradeStationUI();

			// Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
			upgradeStationUI.Activate();
		}

		public override void UpdateUI(GameTime gameTime) {
			// Here we call .Update on our custom UI and propagate it to its state and underlying elements
			if (upgradeStationInterface?.CurrentState != null) {
				upgradeStationInterface?.Update(gameTime);
			}
		}

		// Adding a custom layer to the vanilla layer list that will call .Draw on your interface if it has a state
		// Setting the InterfaceScaleType to UI for appropriate UI scaling
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1) {
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"ExampleMod: Coins Per Minute",
					delegate {
						if (upgradeStationInterface?.CurrentState != null) {
							upgradeStationInterface.Draw(Main.spriteBatch, new GameTime());
						}
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}