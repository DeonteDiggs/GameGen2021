// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
//#################
//#Low Poly Water Shader
//#Author: Botzenhardt3d@gmail.com
//#01/07/2018
//#################

Shader "LowPolyWaterShader"
{
	Properties
	{
		_DistortionBump("Distortion Bump", 2D) = "white" {}
		_DistortionFrequency("DistortionFrequency", Range( 0 , 5)) = 0
		_WaterNormal("WaterNormal", 2D) = "bump" {}
		_NormalScale("NormalScale", Range( 0 , 1)) = 0.3
		_EdgeColor("EdgeColor", Color) = (0.1194853,0.4334433,0.4779412,0)
		_DepthColor("DepthColor", Color) = (0.08715397,0.3823529,0.3701378,0)
		_VirtexDistortion("VirtexDistortion", Range( 0 , 5)) = 0
		_Distortion("Distortion", Range( 0 , 1)) = 0.16
		_WaterDepth("WaterDepth", Range( -5 , 5)) = 0.64
		_WaterFalloff("WaterFalloff", Range( -5 , 0)) = -3.6
		_WorldReflection("World Reflection", CUBE) = "white" {}
		_Reflection("Reflection", Float) = 0
		_WaterGlossiness("WaterGlossiness", Color) = (0.3235294,0.3235294,0.3235294,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityCG.cginc"
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 4.6
		#pragma surface surf Standard alpha:fade keepalpha noshadow exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float4 screenPos;
			float2 uv_texcoord;
			float3 worldRefl;
			INTERNAL_DATA
		};

		uniform float4 _DepthColor;
		uniform float4 _EdgeColor;
		uniform sampler2D _CameraDepthTexture;
		uniform float _WaterDepth;
		uniform float _WaterFalloff;
		uniform sampler2D _GrabTexture;
		uniform float _Distortion;
		uniform float _NormalScale;
		uniform sampler2D _WaterNormal;
		uniform samplerCUBE _WorldReflection;
		uniform float _Reflection;
		uniform float4 _WaterGlossiness;
		uniform sampler2D _DistortionBump;
		uniform float _DistortionFrequency;
		uniform float _VirtexDistortion;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float2 temp_cast_0 = (( ( _Time.y * _DistortionFrequency ) + (ase_vertex3Pos).y )).xx;
			float2 uv_TexCoord101 = v.texcoord.xy * float2( 1,1 ) + temp_cast_0;
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( ( tex2Dlod( _DistortionBump, float4( uv_TexCoord101, 0, 1.0) ).r - 0.5 ) * ( ase_vertexNormal * _VirtexDistortion ) );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float eyeDepth2 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(ase_screenPos))));
			float temp_output_10_0 = saturate( pow( ( abs( ( eyeDepth2 - ase_screenPos.w ) ) + _WaterDepth ) , _WaterFalloff ) );
			float4 lerpResult7 = lerp( _DepthColor , _EdgeColor , temp_output_10_0);
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 appendResult21 = (float2(ase_screenPosNorm.x , ase_screenPosNorm.y));
			float2 uv_TexCoord16 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 panner14 = ( uv_TexCoord16 + 1.0 * _Time.y * float2( -0.01,0 ));
			float2 panner15 = ( uv_TexCoord16 + 1.0 * _Time.y * float2( 0.02,0.02 ));
			float3 temp_output_18_0 = BlendNormals( UnpackScaleNormal( tex2D( _WaterNormal, panner14 ) ,_NormalScale ) , UnpackScaleNormal( tex2D( _WaterNormal, panner15 ) ,_NormalScale ) );
			float4 screenColor20 = tex2D( _GrabTexture, ( float3( ( appendResult21 / ase_screenPosNorm.w ) ,  0.0 ) + ( _Distortion * temp_output_18_0 ) ).xy );
			float4 lerpResult27 = lerp( lerpResult7 , screenColor20 , temp_output_10_0);
			o.Albedo = lerpResult27.rgb;
			float4 temp_cast_3 = (_Reflection).xxxx;
			float4 lerpResult95 = lerp( texCUBE( _WorldReflection, WorldReflectionVector( i , temp_output_18_0 ) ) , temp_cast_3 , float4( 0.0,0,0,0 ));
			o.Metallic = lerpResult95.r;
			o.Smoothness = _WaterGlossiness.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14001
1304;100;608;910;803.0302;925.2145;1;False;False
Node;AmplifyShaderEditor.ScreenPosInputsNode;1;-1869.147,-1207.951;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;2;-1664.147,-1207.951;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-2723.651,-604.7432;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;15;-2423.092,-502.6369;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.02,0.02;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;3;-1584.065,-1129.172;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2432.44,-583.1951;Float;False;Property;_NormalScale;NormalScale;4;0;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;14;-2431.539,-708.7475;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.01,0;False;1;FLOAT;1.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TimeNode;111;-2600.304,-370.1951;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;97;-2589.498,-65.8569;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;110;-2671.201,-172.6623;Float;False;Property;_DistortionFrequency;DistortionFrequency;2;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;12;-2085.2,-735.6946;Float;True;Property;_normalwater;normal-water;3;0;Assets/TessellatedWater/Materials/Textures/normal-water.png;True;0;True;bump;Auto;True;Instance;13;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;4;-1425.047,-1128.202;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-2077.231,-523.5204;Float;True;Property;_WaterNormal;WaterNormal;3;0;Assets/TessellatedWater/Materials/Textures/normal-water.png;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenPosInputsNode;19;-1604.258,-870.3459;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-2271.267,-235.174;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;99;-2350.734,-46.58482;Float;False;False;True;False;True;1;0;FLOAT3;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-1514.142,-1028.222;Float;False;Property;_WaterDepth;WaterDepth;8;0;0.64;-1;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;18;-1769.941,-604.4943;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;100;-2076.055,-247.4206;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1485.652,-700.0611;Float;False;Property;_Distortion;Distortion;7;0;0.16;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-1514.728,-957.1628;Float;False;Property;_WaterFalloff;WaterFalloff;9;0;-3.6;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-1188.828,-998.3504;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;21;-1374.777,-843.3273;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1210.868,-725.2752;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT3;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;101;-1887.444,-283.3199;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;9;-1033.828,-998.3504;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;22;-1205.975,-844.5403;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalVertexDataNode;104;-1252.759,-177.1437;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldReflectionVector;91;-1425.491,-504.9821;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;103;-1607.981,-277.2982;Float;True;Property;_DistortionBump;Distortion Bump;1;0;Assets/TessellatedWater/Materials/Textures/wave-pattern-001.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-1215.027,-1218.089;Float;False;Property;_DepthColor;DepthColor;5;0;0.08715397,0.3823529,0.3701378,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;10;-883.8289,-997.3504;Float;False;1;0;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-978.0264,-1217.09;Float;False;Property;_EdgeColor;EdgeColor;0;0;0.1194853,0.4334433,0.4779412,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-1058.082,-802.9928;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;102;-1297.075,-21.33337;Float;False;Property;_VirtexDistortion;VirtexDistortion;6;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;106;-925.7049,-96.06209;Float;False;2;2;0;FLOAT3;1.0,0,0;False;1;FLOAT;0.0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;92;-1191.117,-513.0764;Float;True;Property;_WorldReflection;World Reflection;10;0;Assets/TessellatedWater/Materials/Cubemap.jpg;True;0;False;white;Auto;False;Object;-1;Auto;Cube;6;0;SAMPLER2D;;False;1;FLOAT3;0,0;False;2;FLOAT;0.0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;93;-869.5073,-393.822;Float;False;Property;_Reflection;Reflection;11;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;7;-711.0251,-1209.089;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenColorNode;20;-741.1362,-862.5071;Float;False;Global;_GrabScreen0;Grab Screen 0;2;0;Object;-1;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;105;-920.3998,-249.7274;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;107;-758.3958,-117.1972;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT3;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;96;-786.501,-684.9908;Float;False;Property;_WaterGlossiness;WaterGlossiness;12;0;0.3235294,0.3235294,0.3235294,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;27;-470.8356,-942.7144;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;95;-696.1081,-509.022;Float;False;3;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;2;COLOR;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-362.6434,-631.71;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;LowPolyWaterShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Transparent;0.5;True;False;0;False;Transparent;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;False;2;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;0;-1;0;0;0;False;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;15;0;16;0
WireConnection;3;0;2;0
WireConnection;3;1;1;4
WireConnection;14;0;16;0
WireConnection;12;1;14;0
WireConnection;12;5;17;0
WireConnection;4;0;3;0
WireConnection;13;1;15;0
WireConnection;13;5;17;0
WireConnection;112;0;111;2
WireConnection;112;1;110;0
WireConnection;99;0;97;0
WireConnection;18;0;12;0
WireConnection;18;1;13;0
WireConnection;100;0;112;0
WireConnection;100;1;99;0
WireConnection;8;0;4;0
WireConnection;8;1;28;0
WireConnection;21;0;19;1
WireConnection;21;1;19;2
WireConnection;25;0;24;0
WireConnection;25;1;18;0
WireConnection;101;1;100;0
WireConnection;9;0;8;0
WireConnection;9;1;29;0
WireConnection;22;0;21;0
WireConnection;22;1;19;4
WireConnection;91;0;18;0
WireConnection;103;1;101;0
WireConnection;10;0;9;0
WireConnection;26;0;22;0
WireConnection;26;1;25;0
WireConnection;106;0;104;0
WireConnection;106;1;102;0
WireConnection;92;1;91;0
WireConnection;7;0;5;0
WireConnection;7;1;6;0
WireConnection;7;2;10;0
WireConnection;20;0;26;0
WireConnection;105;0;103;1
WireConnection;107;0;105;0
WireConnection;107;1;106;0
WireConnection;27;0;7;0
WireConnection;27;1;20;0
WireConnection;27;2;10;0
WireConnection;95;0;92;0
WireConnection;95;1;93;0
WireConnection;0;0;27;0
WireConnection;0;3;95;0
WireConnection;0;4;96;0
WireConnection;0;11;107;0
ASEEND*/
//CHKSM=20DF5F10A00A75D9C015FFE2718FA61AE1EA606D