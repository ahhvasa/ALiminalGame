Shader "Custom/DitheredTransparentURP"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _MinimumAlpha ("Minimum Alpha", Range(0,1)) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }

        Pass
        {
            Name "ForwardLit"

            Tags
            {
                "LightMode" = "UniversalForward"
            }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)

                float4 _Color;
                float4 _MainTex_ST;
                float _Glossiness;
                float _Metallic;
                float _MinimumAlpha;

            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);

                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);

                OUT.screenPos = ComputeScreenPos(OUT.positionHCS);

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

                int2 pixelCoord = int2(screenUV * _ScreenParams.xy);

                static const float dither[16] =
                {
                    0.0625, 0.5625, 0.1875, 0.6875,
                    0.8125, 0.3125, 0.9375, 0.4375,
                    0.2500, 0.7500, 0.1250, 0.6250,
                    1.0000, 0.5000, 0.8750, 0.3750
                };

                int x = pixelCoord.x % 4;
                int y = pixelCoord.y % 4;

                int index = x + y * 4;

                half4 c = SAMPLE_TEXTURE2D(
                    _MainTex,
                    sampler_MainTex,
                    IN.uv
                ) * _Color;

                clip(
                    c.a * 1.1 +
                    _MinimumAlpha -
                    dither[index]
                );

                return half4(c.rgb, 1.0);
            }

            ENDHLSL
        }
    }
}