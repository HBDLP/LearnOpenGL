#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;
uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
	mat4 mvp = projection * view * model;
	// vs_out.PosInWorld = (model * vec4(aPos, 1.0)).xyz;
	// vs_out.TexCoords = aTexCoords;
	
	// mat3 normalMatrix = transpose(inverse(mat3(model)));
	// vs_out.NormalInWorld = normalize( normalMatrix * aNormal);
	TexCoords = aTexCoords;
	gl_Position = vec4(aPos, 1.0);
	// gl_Position.z = 1;
}