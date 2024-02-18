sampler s0;

float timer; // The timer variable to keep track of the effect's duration
float4 FlashColor = float4(1, 1, 1, 1); // White color with full opacity

struct PixelInput
{
    float2 UV : TEXCOORD0;
};

float4 ParryFlash(PixelInput input) : COLOR0
{
    // Sample the existing color from the screen at this pixel
    float4 color = tex2D(s0, input.UV);

    // Calculate the current alpha based on the timer
    float alpha = saturate(1 - (timer * 2)); // We multiply by 2 because we want the effect to last 0.5 seconds

    // Return the color with the flash effect applied
    return lerp(color, FlashColor, alpha);
}

float4 MainPS(float2 texCoord : TEXCOORD0) : COLOR0
{
    return float4(1, 1, 1, 0.5); // Semi-transparent white
}

technique ParryTechnique
{
    pass P0
    {
        PixelShader = compile ps_2_0 MainPS();
    }
}
