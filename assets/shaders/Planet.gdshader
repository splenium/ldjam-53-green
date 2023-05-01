shader_type canvas_item;
#define sat(a) clamp(a, 0., 1.)

uniform vec4 colorA: source_color = vec4(0.000,0.714,0.565,1.);
uniform vec4 colorB: source_color = vec4(0.420,0.243,0.584,1.);

vec4 drawPlanet(vec2 uv, float sz, vec3 a, vec3 b)
{
	vec3 col = vec3(0.);
    float sharp = 40000.;
    float planet = length(uv)-sz;
    

    vec3 n = normalize(vec3(uv.x, 1., uv.y));
    vec3 ldir = normalize(vec3(1.,1.,1.));
        vec3 rgb = mix(a, b, sat((dot(ldir, n)-.485)*10.5));
    col = mix(col, rgb, 1.-sat(planet*sharp));
    col = mix(col, vec3(0.), 1.-sat((abs(planet)-.005)*sharp));
	float opa = 1.-sat((planet-0.005*3.)*sharp);
    return vec4(col, opa);
}
void fragment() {

	vec2 uv = UV-.5;
	float pix = 0.025;
	uv = floor(uv/pix)*pix;
    vec4 col = drawPlanet(uv, .4, 
      colorA.xyz, 
      colorB.xyz);
	COLOR = col;
}
