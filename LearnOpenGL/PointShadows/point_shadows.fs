#version 330 core
out vec4 FragColor;

in VS_OUT{
	vec3 NormalInWorld;
	vec4 PosInWorld;
	vec2 TexCoords;
} fs_in;

uniform vec3 lightPos;
uniform vec3 viewPos;
uniform sampler2D diffuseTexture;
uniform samplerCube depthMap;
uniform float far_plane;

float ShadowCalculation(vec4 fragPos)
{
    vec3 frag2Light = fragPos.xyz - lightPos;
    float closestDepth = texture(depthMap, frag2Light).r;
    closestDepth *= far_plane;
    float currDepth = length(frag2Light);
    // float shadow = currDepth > closestDepth ? 1.0 : 0.0;
    float bias = 0.05; 
    float shadow = currDepth -  bias > closestDepth ? 1.0 : 0.0;
    if(currDepth > far_plane)
    {
        // shadow = 0;
    }

    return shadow;
    // return texture(depthMap, frag2Light).r;
}

void main()
{
    vec3 color = texture(diffuseTexture, fs_in.TexCoords).rgb;
    vec3 lightColor = vec3(0.4);
    vec3 lightDir = normalize(lightPos - fs_in.PosInWorld.xyz);
    vec3 viewDir = normalize(viewPos - fs_in.PosInWorld.xyz);
    vec3 normal = normalize(fs_in.NormalInWorld);

    float ambient = 0.2;

    float diff = max(dot(lightDir, normal), 0);
    vec3 diffuse = lightColor * diff;

    vec3 halfwayDir = normalize(viewDir + lightDir);
    float spec = pow(max(dot(halfwayDir, normal), 0), 64);
    vec3 specular = lightColor * spec;

    float shadow = ShadowCalculation(fs_in.PosInWorld);
    // shadow = 0;
    vec3 lighting = (ambient + (1.0 - shadow) * (diffuse + specular)) * color;

    // FragColor = vec4(shadow,shadow,shadow, 1.0);
    FragColor = vec4(lighting, 1.0);
}