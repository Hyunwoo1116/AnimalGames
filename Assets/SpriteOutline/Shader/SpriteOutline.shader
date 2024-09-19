Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0.0, 0.1)) = 0.05
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 200
        
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float2 uv = i.texcoord;
                half4 original = tex2D(_MainTex, uv);

                // outline offsets (ÁÖº¯ ÇÈ¼¿ »ùÇÃ¸µ)
                float2 offset1 = float2(_OutlineThickness, 0);
                float2 offset2 = float2(0, _OutlineThickness);
                
                half4 outline = original;
                outline.a += tex2D(_MainTex, uv + offset1).a;
                outline.a += tex2D(_MainTex, uv - offset1).a;
                outline.a += tex2D(_MainTex, uv + offset2).a;
                outline.a += tex2D(_MainTex, uv - offset2).a;
                
                // outline drawing condition
                if (original.a == 0 && outline.a > 0)
                {
                    return _OutlineColor;
                }
                return original;
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}