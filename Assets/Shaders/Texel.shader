Shader "Custom/Texel"
{
    Properties
    {
        _MainTex("Texture", 2D) = "purple" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _NormalMap;

        struct Input
        {
            float2 uv_MainTex;   //UV for the main texture
            float2 uv_NormalMap; //UV for the normal map
            float3 worldPos;     //World position
            float3 worldNormal;  //World normal
            float3 viewDir;      //View direction
            INTERNAL_DATA
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Sample textures
            fixed4 col = tex2D(_MainTex, IN.uv_MainTex);
            float3 normalTex = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));

            // Transform normal to world space
            float3 worldNormal = normalize(lerp(o.Normal, normalTex, 1.0));

            // Lighting calculation in texel space
            float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
            float diff = max(0, dot(worldNormal, lightDir));

            float3 halfDir = normalize(lightDir + normalize(IN.viewDir));
            float spec = pow(max(0, dot(worldNormal, halfDir)), 32.0); // Shininess

            o.Albedo = col.rgb * diff; // Apply lighting to texture
            o.Normal = worldNormal;    // Set updated normal
        }
        ENDCG
    }
}