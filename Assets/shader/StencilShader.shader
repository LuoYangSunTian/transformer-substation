Shader "Custom/StencilShader"
{
    Properties
    {
        _StartTime ("startTime", Float) = 0 // _StartTime用于控制每个选中的对象颜色渐变不同步
    }
 
    SubShader
    {
        Pass
        {   
            CGPROGRAM // CG语言的开始
            // 编译指令 着色器名称 函数名称
            #pragma vertex vert // 顶点着色器, 每个顶点执行一次
            #pragma fragment frag // 片段着色器, 每个像素执行一次
            #pragma fragmentoption ARB_precision_hint_fastest // fragment使用最低精度, fp16, 提高性能和速度
 
            // 导入头文件
            #include "UnityCG.cginc"
 
            float _StartTime;
 
            struct a2v // 顶点函数输入结构体
            {
                float4 vertex: POSITION; // 顶点坐标
            };
 
            struct v2f // 顶点函数输出结构体
            {
                float4 pos : SV_POSITION;
            };
            
            v2f vert(a2v v) // 顶点着色器
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            /*fixed4 frag(v2f i) : SV_Target // 片段着色器
            {
                float t1 = sin(_Time.z - _StartTime); // _Time = float4(t/20, t, t*2, t*3)
                float t2 = cos(_Time.z - _StartTime);
                // 描边颜色随时间变化, 描边透明度随时间变化, 视觉上感觉描边在膨胀和收缩
                return float4(t1 + 1, t2 + 1, 1 - t1, 1 - t2);
            }*/
            fixed4 frag(v2f i) : SV_Target
            {
                float t1 = 0.5 * (1 + sin(_Time.z - _StartTime)); // 控制 t1 在 0 到 1 之间变化
                float t2 = 0.5 * (1 + cos(_Time.z - _StartTime)); // 控制 t2 在 0 到 1 之间变化
                
                fixed4 color1 = float4(1,0.337,0,1); // 第一个固定颜色 (例如红色)
                fixed4 color2 = float4(1, 1, 1, 0.9); // 第二个固定颜色 (例如蓝色)
                
                fixed4 finalColor = lerp(color1, color2, t1); // 使用 t1 控制在 color1 和 color2 之间插值
                finalColor.a = lerp(0.5, 1, t2); // 使用 t2 控制透明度在 0.5 和 1 之间插值
                
                return finalColor;
            }
 
            ENDCG // CG语言的结束
        }
    }
 
    FallBack off
}