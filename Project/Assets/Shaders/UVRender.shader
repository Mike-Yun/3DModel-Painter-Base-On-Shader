Shader "Custom/UVRender"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CirclePoint("CirclePoint", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            //ZTest off
            //ZWrite off
            Cull off
            Blend SrcAlpha OneMinusSrcAlpha
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
                float4 worldPosition : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _CirclePoint;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPosition =  mul(unity_ObjectToWorld, v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertex = float4(v.uv * 2 - 1, 1.0, 1.0);
                o.vertex.y = -o.vertex.y;
                //o.vertex = float4(v.uv, 1.0, 1.0);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float3 dir = _CirclePoint.xyz - i.worldPosition.xyz;
                float distance = length(dir);
                if(distance < _CirclePoint.w)
                {
                    float radio = distance / _CirclePoint.w * 0.5;
                    radio = 1 - radio * radio;

                    return float4(0, 0, 1, radio / 10);
                }
                clip(-1);
                return float4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}
