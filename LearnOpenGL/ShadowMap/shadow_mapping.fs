#version 330 core
out vec4 FragColor;

in VS_OUT{
	vec3 NormalInWorld;
	vec4 PosInWorld;
	vec4 PosInLight;
	vec2 TexCoords;
} fs_in;

uniform vec3 lightPos;
uniform vec3 viewPos;
uniform sampler2D diffuseTexture;
uniform sampler2D shadowMap;

float ShadowCalculation(vec4 posLightSpace, vec3 normal, vec3 lightDir)
{
    vec3 lightCoord = posLightSpace.xyz / posLightSpace.w;
    lightCoord = lightCoord * 0.5 + 0.5;


    // float closestDepth = texture(shadowMap, lightCoord.xy).r;
    float currDepth = posLightSpace.z;
    // float bias = max(0.5 * (1 - dot(normal, lightDir)), 0.5);
    float bias = 0;
    float shadow = 0;
    vec2 texelSize = 1.0 / textureSize(shadowMap, 0);
    for(int x= -1; x <= 1; ++x)
    {
        for(int y = -1; y <=1 ; ++y)
        {
            float pcfDepth = texture(shadowMap, lightCoord.xy + vec2(x, y) * texelSize).r;
            shadow += currDepth > pcfDepth ? 1.0 : 0.0;
        }
    }

    shadow /= 9.0;

    if(lightCoord.z > 1.0)
    {
        lightCoord.z = 1.0;
        shadow = 0;
    }
    return shadow;
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

    float shadow = ShadowCalculation(fs_in.PosInLight, normal, lightDir);

    vec3 lighting = (ambient + (1.0 - shadow) * (diffuse + specular)) * color;

    FragColor = vec4(lighting, 1.0);
}