Shader "Custom/HighLight" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _SpecGlossMap("Roughness Map", 2D) = "white" {}
     
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _MetallicGlossMap("Metallic", 2D) = "white" {}
     
        _BumpMap("Normal Map", 2D) = "bump" {}
        _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
     
//        [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
     
        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _SpecGlossMap;
        sampler2D _MetallicGlossMap;
 
        struct Input {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_SpecGlossMap;
            float2 uv_MetallicGlossMap;
            float3 viewDir;
        };
     
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float4 _RimColor;
        float _RimPower;
 
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
 
        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
         
            // Metallic and smoothness come from slider variables
            fixed4 cMetal = tex2D(_MetallicGlossMap, IN.uv_MetallicGlossMap);
            o.Metallic = cMetal.rgb;
                     
            fixed4 cSpec = tex2D(_SpecGlossMap, IN.uv_SpecGlossMap);
            o.Smoothness = _Glossiness * cSpec.a;
                   
            o.Alpha = c.a;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
            o.Emission = _RimColor.rgb * pow (rim, _RimPower);
 
        }
     
        ENDCG
    }
//    FallBack "Diffuse"
}