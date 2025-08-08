Shader "UI/VulkanTest"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _Color ("Color Tint", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // ì¸óÕ
            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            fixed4 _Color;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 texColor = tex2D(_MainTex, i.uv);
                // çïÇéwíËÇµÇΩêFÇ…ïœä∑
                if (texColor.rgb == float3(0, 0, 0))
                    texColor = _Color;
                return texColor;
            }
            ENDCG
        }
    }
    Fallback "Unlit/Texture"
}