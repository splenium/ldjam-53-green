shader_type canvas_item;
#define sat(a) clamp(a, 0., 1.)

vec3 draweyecreature(vec2 uv, vec3 col)
{
    float sharp = 40000.;
    vec2 buv = uv;
    float ban = atan(buv.y, buv.x);
    float blob = length(buv)-.1-.01*sin(ban*7.+TIME)
    -.01*sin(ban*2.-+TIME*.15)
    -.01*sin(ban*15.-+TIME*.2);
    vec2 buve = buv-vec2(sin(+TIME), 0.)*.05;
    
    col = mix(col, vec3(0.), 1.-sat(blob*sharp));
    float eye = length(buve)-.03;
    eye = max(eye, abs(buve.y)-.02*sat(sin(TIME*3.)+.5));
    vec3 innerEye= vec3(1.,0.,0.)*sat((length(buve)-.01)*sharp);
    col = mix(col, innerEye, 1.-sat(eye*sharp));
    return col;
}

void fragment() {
	float pix = .01;
	vec2 uv = UV-.5;
     uv = floor(uv/pix)*pix;
	vec3 col = vec3(0.,1.,0.);
	 col = draweyecreature(uv*.5, col);
	COLOR = vec4(col,float(col.y < 0.5));
}
