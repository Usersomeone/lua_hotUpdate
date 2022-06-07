Shader "Unlit/SolveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineColor("LineColor",Color) = (1,1,1,1)
        _EdgeWidth("EdgeWidth",Range(0,0.9)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _MainTex_TexelSize;
            float4 _LineColor;
            float _EdgeWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                fixed2 left_up_uv = i.uv + _MainTex_TexelSize.xy*fixed2(-1,1);
                fixed2 up_uv = i.uv + _MainTex_TexelSize.xy*fixed2(0,1);
                fixed2 right_up_uv = i.uv + _MainTex_TexelSize.xy*fixed2(1,1);
                fixed2 left_uv = i.uv + _MainTex_TexelSize.xy*fixed2(-1,0);
                fixed2 right_uv = i.uv + _MainTex_TexelSize.xy*fixed2(1,0);
                fixed2 left_down_uv = i.uv + _MainTex_TexelSize.xy*fixed2(-1,-1);
                fixed2 down_uv = i.uv + _MainTex_TexelSize.xy*fixed2(0,-1);
                fixed2 right_down_uv = i.uv + _MainTex_TexelSize.xy*fixed2(1,-1);

                fixed alphaSum = tex2D(_MainTex,left_up_uv).a + tex2D(_MainTex,up_uv).a + tex2D(_MainTex,right_up_uv).a + tex2D(_MainTex,left_uv).a + tex2D(_MainTex,right_uv).a +
                 tex2D(_MainTex,left_down_uv).a + tex2D(_MainTex,down_uv).a + tex2D(_MainTex,right_down_uv).a +tex2D(_MainTex,i.uv).a;
                fixed finalAlpha = alphaSum/9;
                fixed isEdge = finalAlpha > _EdgeWidth;
                 col.rgb = lerp(_LineColor,col.rgb,isEdge);
                return col;
            }
            ENDCG
        }
    }
}
