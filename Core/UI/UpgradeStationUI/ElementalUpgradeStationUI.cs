using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace cherryblossomtest.Core.UI.UpgradeStationUI
{
    internal class ElementalUpgradeStationUI : UIState
    {
        public ElementalUpgradeStationUIPanel panel;

        public override void OnInitialize()
        {
            panel = new ElementalUpgradeStationUIPanel();
            panel.SetPadding(0);
            
            SetRectangle(panel, left: Main.screenWidth / 2 + 100, top: Main.screenHeight / 2 + 250, width: 200f, height: 90f);
            panel.BackgroundColor = new Color(0, 0, 255);

            Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete");
			UIHoverButton closeButton = new UIHoverButton(buttonDeleteTexture, Language.GetTextValue("LegacyInterface.52")); // Localized text for "Close"
			SetRectangle(closeButton, left: 167f, top: 10f, width: 22f, height: 22f);
			closeButton.OnLeftClick += new MouseEvent(CloseButtonClicked);
			panel.Append(closeButton);

            Append(panel);
        }

        private void SetRectangle(UIElement uiElement, float left, float top, float width, float height) {
			uiElement.Left.Set(left, 0f);
			uiElement.Top.Set(top, 0f);
			uiElement.Width.Set(width, 0f);
			uiElement.Height.Set(height, 0f);
		}

        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement) {
			SoundEngine.PlaySound(SoundID.MenuClose);
			ModContent.GetInstance<ElementalUpgradeStationUISystem>().HideMyUI();
		}
    }
}