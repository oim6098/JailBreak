Shader "Custom/BaseMaterial"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _Brightness ("Brightness", Float) = 1.0
        _AmbientColor ("Ambient Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD0;
            };

            fixed4 _Color;
            float _Brightness;
            fixed4 _AmbientColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed3 ambient = _AmbientColor.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb;
                fixed3 norm = normalize(i.normal);
                fixed3 lightDir = normalize(UnityWorldSpaceLightDir(i.pos));
                float diff = max(dot(norm, lightDir), 0.0);
                fixed4 col;
                if(diff > 0.5)
                {
                    col = _Color;
                }
                else if(diff > 0.25)
                {
                    col = _Color * 0.7;
                }
                else
                {
                    col = _Color * 0.5;
                }
                col.rgb *= _Brightness;
                col.rgb += ambient;
                return col;
            }
            ENDCG
        }
    }
}
