#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler TextureSampler : register(s0);
float2 ViewportSize;
float4x4 ScrollMatrix;
struct VertexToPixel {
    float4 Position : SV_Position0;
    float4 Color : COLOR0;
    float4 TexCoord : TEXCOORD0;
};
VertexToPixel SpriteVertexShader(float4 color : COLOR0, float4 texCoord : TEXCOORD0, float4 position : POSITION0) {
    VertexToPixel Output = (VertexToPixel)0;

    // Viewport adjustment.
    position.xy /= ViewportSize;
    position.xy *= float2(2, -2);
    position.xy -= float2(1, -1);

    // Transform our texture coordinates to account for camera
    texCoord = mul(texCoord, ScrollMatrix);

    //pass position and color to PS
    Output.Position = position;
    Output.Color = color;
    Output.TexCoord = texCoord;

    return Output;
}

float4 SpritePixelShader(VertexToPixel PSIn): COLOR0 {
    float4 diffuse = tex2D(TextureSampler , PSIn.TexCoord);
    return PSIn.Color * diffuse ;
}

technique SpriteBatch {
    pass {
        VertexShader = compile VS_SHADERMODEL SpriteVertexShader();
        PixelShader = compile PS_SHADERMODEL SpritePixelShader();
    }
}