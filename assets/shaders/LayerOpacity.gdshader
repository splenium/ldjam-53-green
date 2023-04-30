shader_type canvas_item;
uniform float opacity;
void fragment() {
	vec4 col = texture(TEXTURE, UV);
	col.a *= opacity;
	COLOR = col;
}
