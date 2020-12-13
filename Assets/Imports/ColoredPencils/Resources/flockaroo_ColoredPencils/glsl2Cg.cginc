// glsl -> Cg
// many things can be covered here, but...
// not possible by typedef or #define are:
// (at least not with the limited preprocess abilities of Cg)
//
// array initializers
//       glsl: type arr[n] = type[](a,b,c,...);
//         Cg: type arr[n] = {a,b,c,...}
//
// vec initializers with 1 arg:
//       glsl: vec4(x)
//         Cg: vec4i(x) (see helpers below - or float4(x,x,x,x))
//
// matrix multiplications
//       glsl: m*v
//         Cg: mul(m,v)
//
// matrix initializers (column first in glsl)
//       glsl: mat4(a,b,c,...)
//         Cg: transpose(mat4(a,b,c,...))
//
// in mainGeom(...) vertAttrib[] not initialized
//         so either inout as argument, or actually init them in mainGeom()
//

float4 vec4i(float x) { return float4(x,x,x,x); }
float4 vec4i(float x, float3 v) { return float4(x,v.x,v.y,v.z); }
float4 vec4i(float3 v, float x) { return float4(v.x,v.y,v.z,x); }
float4 vec4i(float2 v1, float2 v2) { return float4(v1.x,v1.y,v2.x,v2.y); }
float3 vec3i(float x) { return float3(x,x,x); }
float3 vec3i(float2 v, float x) { return float3(v.x,v.y,x); }
float3 vec3i(float x, float2 v) { return float3(x,v.x,v.y); }
float2 vec2i(float x) { return float2(x,x); }
//float dFdx(float x) { return ddx(x); }
//float dFdy(float x) { return ddy(x); }

typedef float2   vec2;
typedef float3   vec3;
typedef float4   vec4;
typedef int4     ivec4;
typedef int3     ivec3;
typedef int2     ivec2;
typedef float4x4 mat4;
typedef float3x3 mat3;
typedef float2x2 mat2;

#define atan(a,b) atan2(a,b)
#define texture(a,b) tex2D(a,b)
//#define texture(a,b,c) tex2Dbias(a,float4(b,0,c))
#define textureLod(a,b,c) tex2Dlod(a,float4(b,0,c))
//#define textureLod(a,b,c) tex2D(a,b)
//#define texelFetch(a,b,c) tex2Dfetch(a,int4(b,0,c))
#define texelFetch(a,b,c) tex2Dlod(a,float4((float2(b)+.5)/float2(textureSize(a,0)),0,c))

//#define GET_VARARG_MACRO(_1,_2,_3,_4,NAME,...) NAME
//#define vec4(...) GET_VARARG_MACRO(__VA_ARGS__, vec4i, vec4i, vec4i, vec4i, vec4)(__VA_ARGS__)
//#define vec4 float4
//#define vec3 float3
//#define vec2 float2
//#define ivec4 int4
//#define ivec3 int3
//#define ivec2 int2
//#define mat4 float4x4
//#define mat3 float3x3
//#define mat2 float2x2

#define dFdx(x) ddx(x)
#define dFdy(x) ddy(x)

#define mix(a,b,c) lerp(a,b,c)

#define fract(a) frac(a)
#define mod(a,b) fmod(a,b)

#define textureSize(a,b) a##_TexelSize.zw

//#define smoothstep(a,b,c) step(.5*(a+b),c)
