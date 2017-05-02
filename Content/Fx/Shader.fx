// XNA 4.0 Shader Programming #4 - Normal Mapping
#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Matrix
float4x4 World;
float4x4 View;
float4x4 Projection;

// Light related
float4 AmbientColor;
float AmbientIntensity;

float3 DiffuseLightDirection;
float4 DiffuseColor;
float DiffuseIntensity;

//WorldInverse
float4x4 WorldInverseTranspose;

float4 SpecularColor;
float3 EyePosition;


// The input for the VertexShader
struct VertexShaderInput
{
    float4 Position : SV_POSITION;
	float4 Normal : NORMAL0;
};

// The output from the vertex shader, used for later processing
struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
	float4 Color : COLOR0; 
};

// The VertexShader.
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

	float4 normal = mul(input.Normal, WorldInverseTranspose);
    float lightIntensity = dot(normal, DiffuseLightDirection);
    output.Color = saturate(DiffuseColor * DiffuseIntensity * lightIntensity);

    return output;
}

// The Pixel Shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{

    return saturate(input.Color * AmbientColor * AmbientIntensity);
}

// Our Techinique
technique Technique1
{
    pass Pass1
    {
        VertexShader = compile VS_SHADERMODEL VertexShaderFunction();
        PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
    }
}
