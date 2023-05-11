Shader "Custom/BlinkShader"
{
    Properties {
        _MainColor ("Main Color", Color) = (0.5, 0.5, 0.5, 1)
        _TimeSpeed ("Time Speed", Range(10, 20)) = 15
        _Texture2D ("Texture", 2D) = ""
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Unity built-in variable that holds the position of the vertex in world space
            float4 _WorldSpacePos;

            // Properties
            uniform float4 _MainColor;
            uniform float _TimeSpeed;

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag () : SV_Target {
                float4 color = lerp(float4(0.4, 0.4, 0.4, 1), float4(0.6, 0.6, 0.6, 1), sin(2 * _Time.y));
                return color;
            }

            ENDCG
        }
    }
}