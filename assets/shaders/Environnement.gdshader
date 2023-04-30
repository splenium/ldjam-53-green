shader_type canvas_item;
render_mode blend_add;

uniform vec4 colorA : source_color;
uniform vec4 colorB : source_color;
uniform float opacity;

#define sat(a) clamp(a, 0., 1.)

void fragment() {
	vec3 col = vec3(0.);
	col = mix(colorA.xyz, colorB.xyz, sat(length(UV-.5)));
	
	COLOR = vec4(col*opacity, 1.);
}
