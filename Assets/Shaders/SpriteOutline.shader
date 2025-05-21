Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Float) = 1.0
        _AlphaThreshold ("Alpha Threshold", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineThickness;
            float _AlphaThreshold;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float alpha = tex2D(_MainTex, i.uv).a;

                // Проверка соседних пикселей
                float outline = 0.0;
                float2 offset = float2(_OutlineThickness / _ScreenParams.x, _OutlineThickness / _ScreenParams.y);
                
                for (int x = -1; x <= 1; x++) {
                    for (int y = -1; y <= 1; y++) {
                        float2 shiftedUV = i.uv + float2(x, y) * offset;
                        outline = max(outline, tex2D(_MainTex, shiftedUV).a);
                    }
                }

                float4 col = tex2D(_MainTex, i.uv);
                float finalAlpha = col.a;

                if (outline > _AlphaThreshold && col.a < _AlphaThreshold)
                    return float4(_OutlineColor.rgb, _OutlineColor.a * outline);
                else
                    return col;
            }
            ENDCG
        }
    }
}
