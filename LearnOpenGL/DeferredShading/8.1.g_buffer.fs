#version 330 core
layout (location = 0) out vec3 PosColor;
layout (location = 1) out vec3 NormalColor;
layout (location = 2) out vec4 AlbedoSpecularColor;

in VS_OUT{
	vec3 PosInWorld;
	vec2 TexCoords;
    vec3 NormalInWorld;
} fs_in;


uniform sampler2D texture_diffuse1;
uniform sampler2D texture_specular1;

void main()
{
    vec3 albedoColor = texture(texture_diffuse1, fs_in.TexCoords).rgb;
    float spe = texture(texture_specular1, fs_in.TexCoords).r;
    AlbedoSpecularColor.rgb = albedoColor;
    // AlbedoSpecularColor.rgb = vec3(0.5, 0.5, 0.5);
    AlbedoSpecularColor.a = spe;
    PosColor = fs_in.PosInWorld;
    NormalColor = normalize(fs_in.NormalInWorld);

}