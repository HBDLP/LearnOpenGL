#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out VS_OUT{
	vec3 NormalInWorld;
	vec4 PosInWorld;
	vec4 PosInLight;
	vec2 TexCoords;
} vs_out;


uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;
uniform mat4 lightSpaceMatrix;

void main()
{

	mat4 mvp = projection * view * model;
	vs_out.PosInWorld = model * vec4(aPos, 1.0);
	vs_out.PosInLight = lightSpaceMatrix * vs_out.PosInWorld;
	vs_out.TexCoords = aTexCoords;
	vs_out.NormalInWorld = transpose(inverse(mat3(model))) * aNormal;
	gl_Position = mvp * vec4(aPos, 1.0);


}