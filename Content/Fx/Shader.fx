#if OPENGL
	#define SV_POSITION POSITION0
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix World;
matrix View;
matrix Projection;
float4 AmbientColor;
float AmbientIntensity;
float3 LightDirection;
float SpecularPower;
float4 SpecularColor;
float SpecularIntensity;
float3 ViewVector;

texture ModelTexture;
sampler2D textureSampler = sampler_state {
    Texture = (ModelTexture);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};

float BumpConstant;
texture NormalMap;
sampler2D bumpSampler = sampler_state {
    Texture = (NormalMap);
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};

struct VertexShaderInput
{
	float4 Position : SV_POSITION;
	float3 Normal : NORMAL0;
	float3 Tangent : TANGENT0;
    float3 Binormal : BINORMAL0;
	float2 TextureCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
    float2 TextureCoord : TEXCOORD0;
    float3x3 Tanget : TEXCOORD4;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	//output.Normal = normalize(mul(input.Normal, World));
    //output.Tangent = normalize(mul(input.Tangent, World));
    //output.Binormal = normalize(mul(input.Binormal, World));
    output.Tanget[0] = mul(input.Tangent, World);
    output.Tanget[1] = mul(input.Binormal, World);
    output.Tanget[2] = mul(input.Normal, World);
	output.TextureCoord = input.TextureCoord;
	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR0
{
    //float3 bump = BumpConstant * (tex2D(bumpSampler, input.TextureCoord) - (0.5f, 0.5f, 0.5f));
    //float3 n = input.Normal + (bump.x * input.Tangent + bump.y * input.Binormal);
    //n = normalize(n);
    float3 n = BumpConstant * (tex2D(bumpSampler, input.TextureCoord) - (0.5f, 0.5f, 0.5f));
    n = normalize(mul(n, input.Tanget));

    float diffuseIntensity = max(dot(normalize(LightDirection), n), 0.0f);
    float3 l = normalize(mul(input.Tanget, LightDirection));
    float3 v = normalize(mul(normalize(ViewVector), World));
    float3 h = normalize(l + v);
    float4 specular = SpecularIntensity * SpecularColor * max(pow(dot(n, h), SpecularPower), 0.0f) * diffuseIntensity;
    
    float4 textureColor = tex2D(textureSampler, input.TextureCoord);
    textureColor.a = 1.0f;

    return saturate(textureColor * diffuseIntensity +  AmbientColor * AmbientIntensity + specular);
}

technique BumpMapped
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};