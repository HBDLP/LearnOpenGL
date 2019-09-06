#version 330 core
out vec4 FragColor;
// layout (location = 1) out vec4 BrightnessColor;
in vec2 TexCoords;
in vec4 FragPos;

uniform sampler2D hdrBuffer;
uniform sampler2D brightnessBuffer;
uniform bool hdr;
uniform float exposure;

void main()
{
    const float gamma = 2.2;
    vec3 hdrColor = texture(hdrBuffer, TexCoords).rgb;
    vec3 brightColor = texture(brightnessBuffer, TexCoords).rgb;
    vec3 color = hdrColor + brightColor;
    if(hdr)
    {
        vec3 result = vec3(1.0) - exp(-color * exposure);
        FragColor = vec4(result, 1.0);
    }
    else
    {
        vec3 result = pow(color, vec3(1.0 / gamma));
        FragColor = vec4(result, 1.0);
    }
}