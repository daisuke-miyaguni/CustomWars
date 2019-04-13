Shader "Custom/UI/GaugeShader"
{
	Properties{
		_MainTex("Albedo (RGBA)", 2D) = "white" {}
		_Value("Value", Range(0,1)) = 0
		_FrameTex("FrameAlbedo (RGBA)", 2D) = "white" {}
		_GradationTex("GradationAlbedo(RGB)", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }

		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		LOD 200

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			
			#include "UnityCG.cginc"
			#include "ShaderMyFunc.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			// 頂点座標をカメラの空間に座標変換
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _FrameTex;
			sampler2D _GradationTex;
			fixed _Value;
			
			// ゲージの色を求める。
			// ゲージの溜まり具合がずれている場合はガンマ補正を疑うこと。TextureのsRGBのパラメーターで修正できる
			fixed4 frag (v2f i) : SV_Target
			{
				const half SMOOTH = 0.01f;
				fixed2 input =  fixed2(0.0f, 1.0f);
				fixed2 output = fixed2(-0.001f, 1.0f + SMOOTH);
				fixed normalizedValue = Remap(_Value, input, output);
				fixed4 frameColor = tex2D(_FrameTex, i.uv);
				fixed4 gaugeOriginColor = tex2D(_MainTex, i.uv);
				fixed4 gradationColor = tex2D(_GradationTex, float2(_Value-0.001f, 0));
				half gaugeInnerAlpha = min(gaugeOriginColor.a, smoothstep(gaugeOriginColor.r, gaugeOriginColor.r + SMOOTH, normalizedValue));
				gradationColor.a = gaugeInnerAlpha;

				// ２つのTextureを重ねて表示
				fixed4 c = lerp(frameColor, gradationColor, gradationColor.a);
				clip(c.a - 0.0001f);
				return c;
			}
			ENDCG
		}
	}
}
