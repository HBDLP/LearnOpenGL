#version 330 core
out vec4 FragColor;

// in vec2 TexCoords;
in vec3 Reflect;

// uniform sampler2D texture1;
uniform samplerCube skybox;

void main()
{    
	// vec4 texColor = texture(texture1, TexCoords);
	// if(texColor.a < 0.1)
	// {
	// 	discard;
	// }
    // FragColor = texColor;
	// FragColor = texture(texture1, TexCoords);
	FragColor = texture(skybox, Reflect);

}