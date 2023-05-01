shader_type canvas_item;
render_mode blend_add;
#define sat(a) clamp(a, 0., 1.)

float lenny(vec2 v)
{
    return abs(v.x)+abs(v.y);
}

float hash(float seed)
{
    return fract(sin(seed*123.456)*123.456);
}

vec3 rdr(vec2 uv)
{
    float sharp = 40000.;
    vec3 col = vec3(0.);
    
    float flare = lenny(uv);
    
    vec3 rgb = vec3(1.,0.35,0.2);
    
    col += rgb*(1.-sat(lenny(uv*3.)))*sat(1.+.2*sin(TIME*8.))*sat(-uv.y*5.+.5);
    col += 3.*vec3(.1,.2,.3)*(1.-sat(lenny(uv*vec2(.5,2.)*4.)))*sat(1.+.2*sin(TIME*8.+1.));
    vec2 ouv = uv;
    float rep = .01;
    float id = floor((uv.x+rep*.5)/rep);
    uv.x = mod(uv.x+rep*.5,rep)-rep*.5;
    float light = abs(uv.y+.3-mod(sin(id)-TIME, .25))-.01;
    light = max(light, abs(ouv.x)-.1);
    col += mix(vec3(1.,0.35,0.2),vec3(.1,.2,.3), hash(id))
    *(1.-sat(light*sharp))*sat(ouv.y*2.+.5);
    
    return col;
}

void fragment() {
	vec2 uv = (UV-.5)*vec2(1.,-1.);
	float pix = 0.02;
    uv = floor(uv/pix)*pix;
	COLOR = vec4(rdr(uv),1.);
}
