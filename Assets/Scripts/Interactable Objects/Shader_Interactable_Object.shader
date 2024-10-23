Shader "Unlit/Shader_Interactable_Object"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _Outline ("Outline width", Range (.002, 0.03)) = .005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _OutlineColor;
        float _Outline;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _OutlineColor;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG

        Pass
        {
            Name "Outline"
            Tags { "LightMode" = "Always" }
            Cull Front

            ZWrite On
            ZTest LEqual

            Offset 10,10

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float _Outline;
            float4 _OutlineColor;

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float3 norm = mul((float3x3)unity_ObjectToWorld, v.normal);
                o.pos.xy += norm.xy * _Outline;
                o.color = _OutlineColor;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
