#version 330 core
out vec4 FragColor;

in VS_OUT{
	vec4 FragPos;
}fs_in;

void main()
{
    FragColor = vec4((1 - fs_in.FragPos.zzz), 1.0);
    // FragColor = vec4(color, 1.0);
}