Shader "Custom/JD_camera"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", Float) = 100.0

        _WarpStrength("Warp Strength", Float) = 0.02
        _WarpDropOff("Warp Drop-Off", Float) = 5.0

        _WaveWarpStrength("Wave Warp Strength", Float) = 0.01
        _WaveWarpSpeed("Wave Warp Speed", Float) = 2.0

        _ShadowColor("Shadow Color", Color) = (0.8, 0, 0.8, 1)
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                sampler2D _MainTex;
                float _PixelSize;

                float _WarpStrength;
                float _WarpDropOff;

                float _WaveWarpStrength;
                float _WaveWarpSpeed;

                fixed4 _ShadowColor;

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(float4 vertex : POSITION, float2 uv : TEXCOORD0)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(vertex);
                    o.uv = uv;
                    return o;
                }

                // **Original PS2-Style Warping**
                float2 OriginalWarp(float2 uv)
                {
                    float2 offset = sin(uv * _WarpDropOff) * _WarpStrength;
                    offset *= (1.0 - length(uv) * 0.5); // Drop-off effect
                    return uv + offset;
                }

                // **New Wave-Based Warping**
                float2 WaveWarp(float2 uv, float time)
                {
                    float2 waveOffset;
                    waveOffset.x = sin((uv.x + uv.y) * 10.0 + time * _WaveWarpSpeed) * _WaveWarpStrength;
                    waveOffset.y = cos((uv.x - uv.y) * 10.0 + time * _WaveWarpSpeed) * _WaveWarpStrength;
                    return uv + waveOffset;
                }

                // **Pixelation Function**
                float2 PixelateUV(float2 uv, float size)
                {
                    return floor(uv * size) / size;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float time = _Time.y;

                // Apply both warp effects
                float2 warpedUV = OriginalWarp(i.uv);
                warpedUV = WaveWarp(warpedUV, time);

                // Apply pixelation
                float2 pixelUV = PixelateUV(warpedUV, _PixelSize);

                // Sample the screen texture
                fixed4 col = tex2D(_MainTex, pixelUV);

                // Compute brightness for shadow effect
                float lum = dot(col.rgb, float3(0.3, 0.59, 0.11));
                float shadowFactor = smoothstep(0.0, 0.4, lum);
                col.rgb = lerp(_ShadowColor.rgb, col.rgb, shadowFactor);

                return col;
            }
            ENDCG
        }
        }
}