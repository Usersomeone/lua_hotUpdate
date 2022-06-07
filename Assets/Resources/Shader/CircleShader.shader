Shader "Unlit/CircleShader"
{
    Properties
    {
		   _MainTex("MainTex", 2D) = "white" {}
       _Radius("Radius",Range(0,1)) = 0.5
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
            float _Radius;

            float drawCircle(in fixed2 pos,in fixed2 radius,in float2 uv){
              //return length(p-offset)-r;
              float dis = distance(pos,uv);
              float col = smoothstep(radius+0.05,radius,dis);
              return fixed3(col,col,col);
            }

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
         
                //i.vertex.y = _ScreenParams.y-i.vertex.y;
                //fixed2 p = (2.0*i.vertex- _ScreenParams.xy)/ _ScreenParams.y;
                //fixed2 p = i.uv*2-1.0;
                fixed4 col = tex2D(_MainTex,i.uv);
                i.uv = float2(i.uv.x*2-0.5,i.uv.y);
                //float d = drawCircle(p,fixed2(0.0,0.0),_Radius);
                //if(d<-0.003) col = col*fixed4(1.0,1.0,1.0,1.0);
                //if(d>0.003) col = col*fixed4(0.0,0.0,0.0,1.0);
                //col *= (0.8+0.2*cos(100.0*d));
                fixed3 mask = drawCircle(float2(0.5,0.5),_Radius,i.uv);

                col = col*fixed4(mask,1.0);

                return col;
            }
            ENDCG 

        }
    }
}
