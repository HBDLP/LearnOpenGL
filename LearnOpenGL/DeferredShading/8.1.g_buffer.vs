#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

out VS_OUT{
	vec3 PosInWorld;
	vec2 TexCoords;
	vec3 NormalInWorld;
} vs_out;

void main()
{
	mat4 mvp = projection * view * model;
	vs_out.PosInWorld = (model * vec4(aPos, 1.0)).xyz;
	vs_out.TexCoords = aTexCoords;
	
	mat3 normalMatrix = transpose(inverse(mat3(model)));
	vs_out.NormalInWorld = normalize( normalMatrix * aNormal);

	gl_Position = mvp * vec4(aPos, 1.0);
}