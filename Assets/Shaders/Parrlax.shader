Shader "Custom/Parrlax"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
        _PixelSize("Pixel Size", Float) = 1.0 // this value changes via slider
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf Lambert

            sampler2D _MainTex;
            sampler2D _NormalMap;
            float _PixelSize; // Controls pixelation intensity

            struct Input
            {
                float2 uv_MainTex;   // UV for the main texture
                float2 uv_NormalMap; // UV for the normal map
                float3 worldPos;     // World position
                float3 worldNormal;  // World normal
                float3 viewDir;      // View direction
                INTERNAL_DATA
            };

            // Pixelation Function
            float2 PixelateUV(float2 uv, float size)
            {
                return floor(uv * size) / size; // Snap UV to grid
            }

            void surf(Input IN, inout SurfaceOutput o)
            {
                // Pixelate the UV coordinates
                float2 pixelUV = PixelateUV(IN.uv_MainTex, _PixelSize);
                float2 pixelNormalUV = PixelateUV(IN.uv_NormalMap, _PixelSize);

                // Sample textures with pixelated UVs
                fixed4 col = tex2D(_MainTex, pixelUV);
                float3 normalTex = UnpackNormal(tex2D(_NormalMap, pixelNormalUV));

                // Transform normal to world space
                float3 worldNormal = normalize(lerp(o.Normal, normalTex, 1.0));

                // Lighting calculation in texel space
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float diff = max(0, dot(worldNormal, lightDir));

                o.Albedo = col.rgb * diff; // Apply lighting to texture
                o.Normal = worldNormal;    // Set updated normal
            }
            ENDCG
        }
}