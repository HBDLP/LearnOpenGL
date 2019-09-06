#version 330 core
out vec4 FragColor;

// in VS_OUT{
// 	vec3 PosInWorld;
// 	vec2 TexCoords;
//     vec3 NormalInWorld;
// } fs_in;
in vec2 TexCoords;

struct Light{
    vec3 Position;
    vec3 Color;
        
    float Linear;
    float Quadratic;
};

uniform sampler2D PosMap;
uniform sampler2D NormalMap;
uniform sampler2D AlbedoSpecularMap;

uniform Light lights[32] ;
uniform vec3 viewPos;
void main()
{
    vec3 PosInWorld = texture(PosMap, TexCoords).rgb;
    vec3 NormalInWorld = texture(NormalMap, TexCoords).rgb;
    vec3 AlbedoColor = texture(AlbedoSpecularMap, TexCoords).rgb;
    float specular = texture(AlbedoSpecularMap, TexCoords).a;

    vec3 viewDir = normalize(viewPos - PosInWorld);
   
    vec3 ambient = AlbedoColor * 0.1;
    vec3 lighting = ambient;
    for(int i = 0; i < 32; i++)
    {
        vec3 lightDir = normalize(lights[i].Position - PosInWorld);
        vec3 halfDir = normalize(lightDir + viewDir);

        float diff = max(dot(lightDir, NormalInWorld), 0.0);
        vec3 diffuseColor = ambient * diff * lights[i].Color;
        float spe =  pow(max(dot(halfDir, NormalInWorld), 0.0), 16.0);
        vec3 specularColor = spe * lights[i].Color * specular;
       
        float distance = length(lights[i].Position - PosInWorld);
        float attenuation = 1.0 / (1.0 + lights[i].Linear * distance + lights[i].Quadratic * distance * distance);
        diffuseColor *= attenuation;
        specularColor *= attenuation;

        // lighting += ambient + diffuseColor + specularColor;    
        lighting += diffuseColor + specularColor ;    
    }

    FragColor = vec4(lighting , 1.0);
    // FragColor = vec4(ambient , 1.0);

}