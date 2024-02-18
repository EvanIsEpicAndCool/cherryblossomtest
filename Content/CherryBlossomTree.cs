using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Numerics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace cherryblossomtest.Content;

public class CherryBlossomTree : ModTree
{
    // Custom settings for a cherry blossom tree
    public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
    {
        // You might want to adjust these settings to fit the color theme of cherry blossoms
        UseSpecialGroups = true,
        SpecialGroupMinimalHueValue = 0.9f, // Adjust for pink hue
        SpecialGroupMaximumHueValue = 0.95f,
        SpecialGroupMinimumSaturationValue = 0.5f,
        SpecialGroupMaximumSaturationValue = 0.7f
    };

    public override void SetStaticDefaults()
    {
        // Define the tile type on which the cherry blossom tree grows
        GrowsOnTileId = new int[1] { ModContent.TileType<CherryBlossomBlock>() };
    }

    // Set the trunk texture
    public override Asset<Texture2D> GetTexture()
    {
        return ModContent.Request<Texture2D>("cherryblossomtest/Content/CherryBlossomTree");
    }

    // Set the sapling type for the cherry blossom tree
    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<CherryBlossomSapling>();
    }

    // Additional settings for tree foliage
    public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
    {
        // Custom code for cherry blossom foliage
    }

    // Set branch textures
    public override Asset<Texture2D> GetBranchTextures()
    {
        return ModContent.Request<Texture2D>("cherryblossomtest/Content/CherryBlossomBranches");
    }

    // Set top textures
    public override Asset<Texture2D> GetTopTextures()
    {
        return ModContent.Request<Texture2D>("cherryblossomtest/Content/CherryBlossomTops");
    }

    public override int DropWood()
    {
        throw new System.NotImplementedException();
    }

    // Set leaf type
    public override int TreeLeaf()
    {
        return ModContent.GoreType<CherryBlossomTreeLeaf>();
    }
}
