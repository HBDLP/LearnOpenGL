#version 330 core
out vec4 FragColor;

// in vec2 TexCoords;
in vec3 Reflect;
in vec3 Position;
in vec3 Normal;

// uniform sampler2D texture1;
uniform samplerCube skybox;
uniform vec3 campos;
void main()
{    
	// vec4 texColor = texture(texture1, TexCoords);
	// if(texColor.a < 0.1)
	// {
	// 	discard;
	// }
    // FragColor = texColor;
	// FragColor = texture(texture1, TexCoords);
	vec3 I = normalize(Position - campos);
	vec3 R = reflect(I, Normal);

	FragColor = texture(skybox, R);

}