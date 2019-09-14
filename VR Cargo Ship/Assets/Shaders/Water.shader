Shader "Unlit/Water"
{
    Properties
    {
		_LaneColorA ("Lane A Color", Color) = (0.2, 0.5, 1.0, 0.7)
		_LaneColorB ("Lane B Color", Color) = (0.2, 0.5, 1.0, 0.7)
		_LaneCount ("Lane Count", Int) = 10
		_StartPerc ("Start Percentage", Float) = 0.1
		_EndPerc ("End Percentage", Float) = 0.9
		_CheckerSize ("Checker Size", Float) = 0.02
		_Shininess ("Shininess", Float) = 0.5
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
				float3 worldPosition : TEXCOORD1;
				float3 worldNormal : TEXCOORD2;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _LaneColorA;
			float4 _LaneColorB;
			float _StartPerc;
			float _EndPerc;
			float _CheckerSize;
			int _LaneCount;
			float _Shininess;

            VertToFrag vert (VertexInput v)
            {
                VertToFrag o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            float4 frag (VertToFrag i) : SV_Target
            {
				float3 worldNormal = normalize(i.worldNormal);
				float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                float4 color = _LaneColorA;

				float startPerc = 0.1;
				float endPerc = 0.9;

				float laneArea = ((1.0 - i.uv.y) - _StartPerc) / (_EndPerc - _StartPerc);

				if (laneArea > 1.0) {
					float checker = floor(i.uv.x / _CheckerSize) + floor(i.uv.y / _CheckerSize);

					if (fmod(checker, 2) < 1.0) {
						color = _LaneColorA;
					} else {
						color = _LaneColorB;
					}
				} else if (laneArea > 0.0) {
					float laneFactor = laneArea * _LaneCount;

					if (fmod(laneFactor, 2) >= 1.0) {
						color = _LaneColorA;
					} else {
						color = _LaneColorB;
					}
				}

				float diffuseFactor = max(0, dot(worldNormal, lightDir));
				float3 diffuse = _LightColor0 * diffuseFactor;

				float3 lightReflectionDir = normalize(2 * dot(lightDir, worldNormal) * worldNormal - lightDir);
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPosition.xyz);
				float specularFactor = pow(max(0.0, dot(lightReflectionDir, viewDir)), _Shininess) * 0.2;

				color.xyz = float3(1.0, 1.0, 1.0) * specularFactor + color.xyz * diffuse;
				color.w = (1.0 - color.w) * specularFactor + color.w;

                UNITY_APPLY_FOG(i.fogCoord, color);
                return color;
            }
            ENDCG
        }
    }
}
