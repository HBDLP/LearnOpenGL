#version 330 core
layout (location = 0) in vec3 aPos;
// layout (location = 1) in vec2 aTexCoords;
layout (location = 1) in vec3 aNormal;

// out vec2 TexCoords;
out vec3 Reflect;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 campos;

void main()
{
    // TexCoords = aTexCoords;   
    vec3 Normal = normalize(mat3(transpose(inverse(model))) * aNormal);
    // vec3 Position = normalize(campos - aPos);
    vec3 Position = vec3(model * vec4(aPos, 1.0));
    Position = normalize(Position - campos);
    Reflect = reflect(Position, Normal);

    gl_Position = projection * view * model * vec4(aPos, 1.0);

}