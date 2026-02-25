Shader "Custom/UVScrollWithSpriteMask"
{
    Properties
    {
        _MaskTex ("Mask Texture (Sprite Alpha)", 2D) = "white" {}
        _PatternTex ("Pattern Texture (Repeat)", 2D) = "white" {}
        _ScrollX ("Scroll Speed X", Float) = 0.1
        _ScrollY ("Scroll Speed Y", Float) = 0.0
        _Rotate ("Pattern Rotation (Degree)", Float) = 0
        _Frequency ("Pattern Frequency", Float) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MaskTex;
            sampler2D _PatternTex;
            float _ScrollX;
            float _ScrollY;
            float _Rotate;
            float _Frequency;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // マスク（形）
                fixed4 mask = tex2D(_MaskTex, i.uv);

                // --- UVスケール（頻度） ---
                float2 uv = i.uv - 0.5;
                uv *= _Frequency;

                // --- UV回転 ---
                float rad = radians(_Rotate);
                float s = sin(rad);
                float c = cos(rad);
                uv = float2(
                    uv.x * c - uv.y * s,
                    uv.x * s + uv.y * c
                );

                uv += 0.5;

                // --- スクロール ---
                uv += float2(_ScrollX, _ScrollY) * _Time.y;

                // 模様
                fixed4 pattern = tex2D(_PatternTex, uv);

                // マスクで切り抜き
                pattern.a *= mask.a;
                return pattern;
            }
            ENDCG
        }
    }
}