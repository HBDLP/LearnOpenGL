#version 330 core
out vec4 FragColor;

in VS_OUT{
	vec3 PosInWorld;
	vec2 TexCoords;
    vec3 NormalInWorld;
} fs_in;

struct Light{
    vec3 Position;
    vec3 Color;
};

uniform sampler2D diffuseMap;
// uniform sampler2D normalMap;
// uniform sampler2D depthMap;
// uniform bool normalMapping;
// uniform float heightScale;
uniform Light lights[16] ;



void main()
{;
    vec3 color = texture(diffuseMap, fs_in.TexCoords).rgb;
    vec3 ambient = 0.1 * color;
    vec3 lighting = vec3(0.0);
    for(int i = 0; i < 16; i++)
    {

        vec3 lightDir = normalize(lights[i].Position - fs_in.PosInWorld);
        float diff = max(dot(lightDir, fs_in.NormalInWorld), 0.0);
        vec3 result = lights[i].Color * diff * color;
        float distance = length(fs_in.PosInWorld - lights[i].Position);
        result *= 1.0 / (distance * distance + 2 * distance);
        lighting += result;
        // lighting += diffuse;
    }

    FragColor = vec4(ambient + lighting , 1.0);
    // FragColor = vec4(1, 1, 1 , 1.0);
}