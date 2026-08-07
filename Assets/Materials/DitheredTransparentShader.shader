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
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float4 screenPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _MinimumAlpha;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

            float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
            int2 pixelCoord = int2(screenUV * _ScreenParams.xy);


            static const float dither[16] = {
                0.0625, 0.5625, 0.1875, 0.6875,
                0.8125, 0.3125, 0.9375, 0.4375,
                0.2500, 0.7500, 0.1250, 0.6250,
                1.0000, 0.5000, 0.8750, 0.3750
            };


            int x = pixelCoord.x % 4;
            int y = pixelCoord.y % 4;


            int index = x + y * 4;


            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            clip(c.a * 1.1 + _MinimumAlpha - dither[index]);

            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1.0; 
        }
        ENDCG
    }
    FallBack "Diffuse"
}