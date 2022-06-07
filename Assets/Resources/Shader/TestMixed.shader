Shader "Unlit/TestMixed"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Alpha("Alpha",Range(0,1)) = 0.5
        _EdgeAlphaThreshold("Edge Alpha Threshold",Float)=1.0
        _EdgeColor("Edge Color",Color) =(0,0,0,1)
        _EdgeDampRate("Edge Damp Rate",Float)=2
        _OriginAlphaThreshold("OriginAlphaThreshold",Range(0.1,1))=0.2
       // [Toggle(_ShowOutline)] _DualGrid("Show Outline",Int)=0


    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        ZWrite Off
        Cull Off

        Blend SrcAlpha OneMinusSrcAlpha

         Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv[9] : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Alpha;
            fixed _EdgeAlphaThreshold;
            fixed4 _EdgeColor;
            float _EdgeDampRate;
            float _OriginAlphaThreshold;

            half CalculateAlphaSumAround(v2f i){
              half texAlpha;
              half alphaSum = 0;
              for(int it=0; it<9; it++){
                texAlpha = tex2D(_MainTex,i.uv[it]).w;
                alphaSum = alphaSum + texAlpha;
              }
              return alphaSum;
            }

            v2f vert (appdata_img v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                half2 uv = v.texcoord;

                o.uv[0]= uv+ _MainTex_TexelSize.xy*half2(-1,-1);
                o.uv[1]= uv+ _MainTex_TexelSize.xy*half2(0,-1);
                o.uv[2]= uv+ _MainTex_TexelSize.xy*half2(1,-1);
                o.uv[3]= uv+ _MainTex_TexelSize.xy*half2(-1,0);
                o.uv[4]= uv+ _MainTex_TexelSize.xy*half2(0,0);
                o.uv[5]= uv+ _MainTex_TexelSize.xy*half2(1,0);
                o.uv[6]= uv+ _MainTex_TexelSize.xy*half2(-1,1);
                o.uv[7]= uv+ _MainTex_TexelSize.xy*half2(0,1);
                o.uv[8]= uv+ _MainTex_TexelSize.xy*half2(1,1);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
             // #if defined(_ShowOutline)
              half alphaSum = CalculateAlphaSumAround(i);
              float isNeedShow = alphaSum>_EdgeAlphaThreshold;
              float damp = saturate((alphaSum-_EdgeAlphaThreshold)*_EdgeDampRate);
              fixed4 origin = tex2D(_MainTex,i.uv[4]);
              float isOrigin = origin.a>_OriginAlphaThreshold;
              fixed3 finalColor = lerp(_EdgeColor.rgb,origin.rgb,isOrigin);

              return fixed4(finalColor.rgb,isNeedShow*damp);

            //  #endif
                // sample the texture
             /*    fixed4 col = tex2D(_MainTex, i.uv[4]);
                col = col*_Alpha;
                return col; */
            }
            ENDCG
        }
 
    }
}
