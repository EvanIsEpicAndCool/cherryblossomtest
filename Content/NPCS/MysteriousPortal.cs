using Terraria;
using Terraria.ModLoader;
using SubworldLibrary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;

namespace cherryblossomtest.Content.NPCS
{
    public class MysteriousPortal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true; // This will be changed once the NPC is spawned
            NPC.friendly = true;
            NPC.width = 50;
            NPC.height = 52;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0;
        }
        public override string GetChat()
        {
            SubworldSystem.Enter<CherryBlossomSubworld>();
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add(Language.GetTextValue("l"));
            string chosenChat = chat;
            return chosenChat;
        }


        public override void SetChatButtons(ref string button, ref string button2)
        { // What the chat buttons are when you open up the chat UI
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                
            }
        }
    }
}