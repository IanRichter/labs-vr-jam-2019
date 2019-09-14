Shader "Unlit/Water"
{
    Properties
    {
		_BaseColor ("Base Color", Color) = (0.2, 0.5, 1.0, 0.7)
		_LaneColorA ("Lane A Color", Color) = (0.2, 0.5, 1.0, 0.7)
		_LaneColorB ("Lane B Color", Color) = (0.2, 0.5, 1.0, 0.7)
		_LaneCount ("Lane Count", Int) = 10
		_StartPerc ("Start Percentage", Float) = 0.1
		_EndPerc ("End Percentage", Float) = 0.9
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
				float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct VertToFrag
            {
                float2 uv : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _BaseColor;
			float4 _LaneColorA;
			float4 _LaneColorB;
			float _StartPerc;
			float _EndPerc;
			int _LaneCount;

            VertToFrag vert (VertexInput v)
            {
                VertToFrag o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            float4 frag (VertToFrag i) : SV_Target
            {
                float4 color = _LaneColorA;

				float startPerc = 0.1;
				float endPerc = 0.9;

				float laneArea = ((1.0 - i.uv.y) - _StartPerc) / (_EndPerc - _StartPerc);

				if (laneArea > 0.0 && laneArea < 1.0) {
					float laneFactor = laneArea * _LaneCount;

					if (fmod(laneFactor, 2) < 1.0) {
						color = _LaneColorA;
					} else {
						color = _LaneColorB;
					}
				}

				float diffuseFactor = max(0, dot(i.worldNormal, _WorldSpaceLightPos0.xyz));
				float3 diffuse = _LightColor0 * diffuseFactor;

				color.xyz *= diffuse;

                UNITY_APPLY_FOG(i.fogCoord, color);
                return color;
            }
            ENDCG
        }
    }
}
