shader_type canvas_item;
uniform sampler2D maskTexture;
#define sat(a) clamp(a, 0., 1.)
void fragment(){
	float sharp = 40000.;
	vec2 uv = UV;
	uv -= .5;
	float scaling = 1.+sin(TIME)*.1;
	uv = uv*scaling;
	uv += .5;
	vec4 col = texture(TEXTURE, uv);
	float mask = texture(maskTexture, uv).x;
	float lightDist = abs(-uv.x-uv.y*.5+mod(TIME*2., 4.)-2.)-.1;
	col += vec4(1.)*(1.-sat(lightDist*sharp))*mask;
  COLOR = col;
}
