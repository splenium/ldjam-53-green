shader_type canvas_item;
render_mode blend_add;

uniform vec4 SunColor;

#define sat(a) clamp(a, 0., 1.)
float lenny(vec2 v)
{
    return abs(v.x)+abs(v.y);
}
void fragment() {
	vec3 col = vec3(0.);
	vec2 suv = (UV-.5)*3.;
	float pix = .025;
	suv = floor(suv/pix)*pix;
	   float sun = lenny(suv);
   
    col += sat(1.+.2*sin(TIME*5.))*SunColor.xyz*(1.-sat(lenny(suv*3.)));
    col += sat(1.+.2*sin(TIME*2.))*SunColor.xyz*(1.-sat(lenny(suv*2.)));
    col += sat(1.+.2*sin(TIME*3.))*SunColor.xyz*(1.-sat(lenny(suv*15.*vec2(.05,1.))));
    
	COLOR = vec4(col, 1.);
}
