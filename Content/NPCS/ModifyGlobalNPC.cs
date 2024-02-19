using System;
using cherryblossomtest.Content.ElementDamageClasses;
using static cherryblossomtest.Content.ElementDamageClasses.ElementEnumberables;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Server;

namespace cherryblossomtest.Content.NPCS
{
    public class ModifyGlobalNPC : GlobalNPC
    {
        ElementTypes elementType = ElementTypes.Nature;

        // creates a new instance of ModifyGlobalNPC for every NPC 
        //that way we can save unique tags on every NPC in 
        // the world
        public override bool InstancePerEntity => true;
        public static ModifyGlobalNPC instance;

        public ModifyGlobalNPC()
        {
            instance = this;
        }

        public ElementTypes GetElementType()
        {
            return elementType;
        }

        public void SetElementType(ElementTypes newElementType)
        {
            elementType = newElementType;
        }

        public override void SaveData(NPC npc, TagCompound tag)
        {
            if(elementType != null && npc.type == NPCID.Zombie)
            {
                tag.Add("elementType", elementType);
            }
        }

        public override void LoadData(NPC npc, TagCompound tag)
        {
            elementType = tag.Get<ElementTypes>("elementType");
        }
    }

}