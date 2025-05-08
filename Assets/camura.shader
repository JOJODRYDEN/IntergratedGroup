Shader "Custom/camura"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", Float) = 100.0
        _Ramp("Lighting Ramp", 2D) = "white" {}
        _ShadowColor("Shadow Color", Color) = (0.8, 0, 0.8) // New shadow color
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                sampler2D _MainTex;   // Camera texture
                sampler2D _Ramp;      // Cel-shading ramp
                float _PixelSize;     // Pixel size (for pixelation effect)
                fixed4 _ShadowColor;  // Custom shadow color

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                // Pixelate function
                float2 PixelateUV(float2 uv, float size)
                {
                    return floor(uv * size) / size;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Apply pixelation to UV
                    float2 pixelUV = PixelateUV(i.uv, _PixelSize);

                    // Sample the scene color
                    fixed4 col = tex2D(_MainTex, pixelUV);

                    // Compute brightness (luminance approximation)
                    float lum = dot(col.rgb, float3(0.3, 0.59, 0.11));

                    // Sample the ramp texture to get cel shading levels
                    float step = tex2D(_Ramp, float2(lum, 0.5)).r;

                    // Blend shadow color: if dark, lerp to _ShadowColor
                    float shadowFactor = smoothstep(0.0, 0.4, step); // Controls shadow range
                    col.rgb = lerp(_ShadowColor.rgb, col.rgb, shadowFactor);

                    return col;
                }
                ENDCG
            }
        }
}