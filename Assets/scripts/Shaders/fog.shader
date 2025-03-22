Shader "Custom/fog"
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _FogColor("Fog Color", Color) = (1,1,1,1)
        _FogStart("Fog Start", Float) = 0.0
        _FogDepth("Fog Depth", Float) = -5.0
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 150

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd

        sampler2D _MainTex;
            half3 _FogColor;
            float _FogStart;
            float _FogDepth;


        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            float clampedPos = clamp((IN.worldPos.y - _FogStart) / _FogDepth, 0.0, 1.0); // preparing "bw"ramp
            o.Albedo = c * (1 - clampedPos); // apply inverted ramp to Texture
            o.Emission = _FogColor * clampedPos; // apply ramp to FogColor

        }
        ENDCG
        }
}