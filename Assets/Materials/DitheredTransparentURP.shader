Shader "Custom/DitheredTransparentShader"
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
            "Queue" = "Geometry"
            "RenderPipeline" = "UniversalPipeline"
        }

        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            HLSLPROGRAM

            #pragma target 3.0

            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_SCREEN

            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS

            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS

            #pragma multi_compile _ _SHADOWS_SOFT

            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)

                float4 _Color;
                float4 _MainTex_ST;

                half _Glossiness;
                half _Metallic;
                half _MinimumAlpha;

            CBUFFER_END


            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };


            struct Varyings
            {
                float4 positionCS : SV_POSITION;

                float3 positionWS : TEXCOORD0;
                half3 normalWS    : TEXCOORD1;

                float2 uv         : TEXCOORD2;

                float4 shadowCoord : TEXCOORD3;

                half fogFactor : TEXCOORD4;

                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };


            Varyings vert(Attributes IN)
            {
                Varyings OUT = (Varyings)0;

                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                VertexPositionInputs positionInputs =
                    GetVertexPositionInputs(IN.positionOS.xyz);

                VertexNormalInputs normalInputs =
                    GetVertexNormalInputs(IN.normalOS);

                OUT.positionCS = positionInputs.positionCS;
                OUT.positionWS = positionInputs.positionWS;
                OUT.normalWS = normalInputs.normalWS;

                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);

                OUT.shadowCoord =
                    GetShadowCoord(positionInputs);

                OUT.fogFactor =
                    ComputeFogFactor(positionInputs.positionCS.z);

                return OUT;
            }


            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                half4 c =
                    SAMPLE_TEXTURE2D(
                        _MainTex,
                        sampler_MainTex,
                        IN.uv
                    ) * _Color;


                float2 screenUV =
                    IN.positionCS.xy / IN.positionCS.w;

                int2 pixelCoord =
                    int2(screenUV * _ScreenParams.xy);

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


                clip(
                    c.a * 1.1
                    + _MinimumAlpha
                    - dither[index]
                );

                SurfaceData surfaceData = (SurfaceData)0;

                surfaceData.albedo = c.rgb;
                surfaceData.metallic = _Metallic;
                surfaceData.specular = half3(0, 0, 0);
                surfaceData.smoothness = _Glossiness;

                surfaceData.normalTS = half3(0, 0, 1);

                surfaceData.occlusion = 1.0;
                surfaceData.emission = half3(0, 0, 0);

                surfaceData.alpha = 1.0;

                surfaceData.clearCoatMask = 0.0;
                surfaceData.clearCoatSmoothness = 0.0;

                InputData inputData = (InputData)0;

                inputData.positionWS =
                    IN.positionWS;

                inputData.normalWS =
                    NormalizeNormalPerPixel(IN.normalWS);

                inputData.viewDirectionWS =
                    GetWorldSpaceNormalizeViewDir(
                        IN.positionWS
                    );

                inputData.shadowCoord =
                    IN.shadowCoord;

                inputData.fogCoord =
                    InitializeInputDataFog(
                        float4(IN.positionWS, 1.0),
                        IN.fogFactor
                    );

                inputData.bakedGI =
                    SampleSH(inputData.normalWS);


                half4 color =
                    UniversalFragmentPBR(
                        inputData,
                        surfaceData
                    );


                color.rgb =
                    MixFog(
                        color.rgb,
                        inputData.fogCoord
                    );

                color.a = 1.0;

                return color;
            }

            ENDHLSL
        }

        Pass
        {
            Name "ShadowCaster"

            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            ZWrite On
            ZTest LEqual
            ColorMask 0

            HLSLPROGRAM

            #pragma target 3.0

            #pragma vertex ShadowVert
            #pragma fragment ShadowFrag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)

                float4 _Color;
                float4 _MainTex_ST;

                half _Glossiness;
                half _Metallic;
                half _MinimumAlpha;

            CBUFFER_END


            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };


            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv         : TEXCOORD0;
            };


            Varyings ShadowVert(Attributes IN)
            {
                Varyings OUT;

                OUT.positionCS =
                    TransformObjectToHClip(
                        IN.positionOS.xyz
                    );

                OUT.uv =
                    TRANSFORM_TEX(
                        IN.uv,
                        _MainTex
                    );

                return OUT;
            }


            half4 ShadowFrag(Varyings IN) : SV_Target
            {
                half4 c =
                    SAMPLE_TEXTURE2D(
                        _MainTex,
                        sampler_MainTex,
                        IN.uv
                    ) * _Color;

                clip(
                    c.a * 1.1
                    + _MinimumAlpha
                    - 0.5
                );

                return 0;
            }

            ENDHLSL
        }
    }

    FallBack Off
}
