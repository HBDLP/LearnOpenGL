#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;
layout (location = 3) in vec3 aTangent;
layout (location = 4) in vec3 aBitanent;

out VS_OUT{
	vec3 PosInWorld;
	vec2 TexCoords;
	vec3 TangentLightPos;
	vec3 TangentViewPos;
	vec3 TangentFragPos;
} vs_out;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;
uniform vec3 lightPos;
uniform vec3 viewPos;

void main()
{
	mat4 mvp = projection * view * model;
	vs_out.PosInWorld = (model * vec4(aPos, 1.0)).xyz;
	vs_out.TexCoords = aTexCoords;
	
	mat3 normalMatrix = transpose(inverse(mat3(model)));
	vec3 T = normalize(normalMatrix * aTangent);
	vec3 B = normalize(normalMatrix * aBitanent);
	vec3 N = normalize(normalMatrix * aNormal);

	mat3 TBN = transpose(mat3(T, B, N));
	vs_out.TangentLightPos = TBN * lightPos;
	vs_out.TangentViewPos = TBN * viewPos;
	vs_out.TangentFragPos = TBN * vs_out.PosInWorld;
	
	gl_Position = mvp * vec4(aPos, 1.0);
}