Shader "Custom/FadeInOutColor" {
    Properties {
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
        _FadeColor ("Fade Color", Color) = (0, 0, 0, 1)
        _NearDistance ("Near Distance", Range(0, 10)) = 1
        _FarDistance ("Far Distance", Range(0, 10)) = 2
    }

    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass {
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
                float distance : TEXCOORD1;
            };

            float4 _BaseColor;
            float4 _FadeColor;
            float _NearDistance;
            float _FarDistance;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.distance = length(UnityObjectToViewPos(v.vertex));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Calculate the fade factor based on the distance
                float fadeFactor = saturate((i.distance - _NearDistance) / (_FarDistance - _NearDistance));
                
                // Interpolate the fade color and set the alpha channel
                fixed4 fadeColor = lerp(_BaseColor, _FadeColor, fadeFactor);
                fadeColor.a = fadeFactor;

                return fadeColor;
            }
            ENDCG
        }
    }
}
