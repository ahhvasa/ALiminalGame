Shader "Unlit/PosterizeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _Levels ("Levels", Float) = 4

        _PixelSize ("Pixel Size", Float) = 320

        _Dithering ("Dithering", Range(0, 0.2)) = 0.1
        _DitheringDarknessMultiplyer ("DitheringDarknessMultiplyer", Range(0, 1)) = 0.1
        _DitherPixelSize ("Dither Pixel Size", Float) = 4

        _OutlineThreshold ("Outline Threshold", Range(0.0001, 0.1)) = 0.01
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }

        Pass
        {
            ZTest Always
            ZWrite Off
            Cull Off

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;

            float _Levels;

            float _PixelSize;

            float _Dithering;
            float _DitheringDarknessMultiplyer;
            float _DitherPixelSize;

            float _OutlineThreshold;
            float4 _OutlineColor;


            float Bayer4x4(int x, int y)
            {
                if (x == 0 && y == 0) return 0.0 / 16.0;
                if (x == 1 && y == 0) return 8.0 / 16.0;
                if (x == 2 && y == 0) return 2.0 / 16.0;
                if (x == 3 && y == 0) return 10.0 / 16.0;

                if (x == 0 && y == 1) return 12.0 / 16.0;
                if (x == 1 && y == 1) return 4.0 / 16.0;
                if (x == 2 && y == 1) return 14.0 / 16.0;
                if (x == 3 && y == 1) return 6.0 / 16.0;

                if (x == 0 && y == 2) return 3.0 / 16.0;
                if (x == 1 && y == 2) return 11.0 / 16.0;
                if (x == 2 && y == 2) return 1.0 / 16.0;
                if (x == 3 && y == 2) return 9.0 / 16.0;

                if (x == 0 && y == 3) return 15.0 / 16.0;
                if (x == 1 && y == 3) return 7.0 / 16.0;
                if (x == 2 && y == 3) return 13.0 / 16.0;

                return 5.0 / 16.0;
            }


            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex);

                return o;
            }


            float2 GetPixelatedUV(float2 uv)
            {
                float2 pixelCount = float2
                (
                    _PixelSize,
                    _PixelSize * (_ScreenParams.y / _ScreenParams.x)
                );

                return floor(uv * pixelCount) / pixelCount;
            }


            float GetBrightness(float3 color)
            {
                return dot(color, float3(0.299, 0.587, 0.114));
            }


            float GetDarkness(float brightness)
            {
                return lerp
                (
                    1.0,
                    1.0 - brightness,
                    _DitheringDarknessMultiplyer
                );
            }


            float GetDitherThreshold(float2 screenUV)
            {
                float2 pixelPos = screenUV * _ScreenParams.xy;

                int x = (int)(pixelPos.x / _DitherPixelSize) % 4;
                int y = (int)(pixelPos.y / _DitherPixelSize) % 4;

                return Bayer4x4(x, y);
            }


            float3 ApplyDithering(float3 color, float2 screenUV)
            {
                float brightness = GetBrightness(color);

                float darkness = GetDarkness(brightness);

                float threshold = GetDitherThreshold(screenUV);

                color += threshold * _Dithering * darkness;

                return color;
            }


            float3 ApplyPosterization(float3 color)
            {
                return floor(color * _Levels) / _Levels;
            }


            float GetDepth(float2 uv)
            {
                return Linear01Depth
                (
                    SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv)
                );
            }


            float DetectOutline(float2 uv)
            {
                float2 texel = 1.0 / _ScreenParams.xy;

                float center = GetDepth(uv);

                float left  = GetDepth(uv - float2(texel.x, 0));
                float right = GetDepth(uv + float2(texel.x, 0));

                float up    = GetDepth(uv + float2(0, texel.y));
                float down  = GetDepth(uv - float2(0, texel.y));

                float diff = 0;

                diff += abs(center - left);
                diff += abs(center - right);
                diff += abs(center - up);
                diff += abs(center - down);


                return step(_OutlineThreshold, diff);
            }


            fixed4 frag(v2f i) : SV_Target
            {
                // Pixelation
                float2 pixelatedUV = GetPixelatedUV(i.uv);

                fixed4 col = tex2D(_MainTex, pixelatedUV);

                // Dithering
                col.rgb = ApplyDithering(col.rgb, i.uv);

                // Posterization
                col.rgb = ApplyPosterization(col.rgb);

                // Outline
                float outline = DetectOutline(pixelatedUV);

                col.rgb = lerp(col.rgb, _OutlineColor.rgb, outline);

                return col;
            }

            ENDCG
        }
    }
}