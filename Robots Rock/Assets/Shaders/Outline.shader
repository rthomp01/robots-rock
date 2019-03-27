Shader "Custom/Outline"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
		_Color("Tint Color", Color) = (1,1,1,1)
		[Toggle(DRAW_OUTLINE)] _Outline("Draw Outline", Float) = 0
		_OutlineWidth("Outline Width", Range(0.0, 0.2)) = 0.05
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
	}

	CGINCLUDE
		#include "UnityCG.cginc"

		struct appdata 
		{
			float4 vertex : POSITION;
		};

		struct v2f
		{
			float4 pos : POSITION;
		};

		sampler2D _MainTex;
		float4 _Color;
		float _OutlineWidth;
		float4 _OutlineColor;

	ENDCG
	
	SubShader
	{
		PASS//Outline
		{
			Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}

			//Not writing to depth buffer so when we draw our opaque geometry
			//after this pass, it will overwrite all but the edges of the "outline"
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature DRAW_OUTLINE

			//TODO: offer support for multiple methods here. This method 
			//looks pretty good on basic uniform objects but not as good on 
			//complicated surfaces.
			v2f vert(appdata v)
			{
				v2f o;
				//Expand verts to be slightly bigger than actual object
				v.vertex.xyz += v.vertex.xyz * _OutlineWidth;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				#ifndef DRAW_OUTLINE
					discard;
				#endif

				//Render slightly bigger object as solid color
				return _OutlineColor;
			}
			ENDCG
		}

		Tags{ "Queue" = "Geometry"}

		CGPROGRAM //Geometry
			#pragma surface surf Lambert

			struct Input
			{
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput o)
			{
				//Bare mininum Texture * Tint color 
				//TODO: add support for metallic/smoothness etc
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
		ENDCG
	}
		CustomEditor "OutlineShaderGUI"
		Fallback "Diffuse"
}