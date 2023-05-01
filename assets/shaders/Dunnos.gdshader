shader_type canvas_item;

#define sat(a) clamp(a, 0., 1.)

// Thanks IQ :)
float sdSegment( in vec2 p, in vec2 a, in vec2 b )
{
    vec2 pa = p-a, ba = b-a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
    return length( pa - ba*h );
}
mat2 r2d(float a){
    float c = cos(a), s = sin(a);
    return mat2(vec2(c, -s), vec2(s, c));
}
vec4 dunno(vec2 uv, float seed, vec4 col)
{
    uv*= r2d(uv.y*sin(seed));
    uv.y += sat(sin(TIME*.25+seed)*3.);
    uv.y += sin(seed*5.3)*.1;
    uv *= r2d(sin(seed*115.)*.1);
    float sharp = 40000.;
    float w = mix(0.015,0.07, fract(sin(seed*13.3)*133.33));
    float body = sdSegment(uv, vec2(0.,-.5), vec2(0.,-.1))-w;
    col.xyz = mix(col.xyz, vec3(0.), 1.-sat(body*sharp));

    uv.x = abs(uv.x+sin(TIME+seed)*0.01)-.02;
    float eye = length(uv-vec2(0.,-.1))-.01;
    col.xyz = mix(col.xyz, vec3(0.996,0.647,0.043), 1.-sat(eye*sharp));
    return vec4(col.xyz, sat(col.w+1.-sat(body*sharp)));
}

void fragment() {
	    float pix = 0.005;
	vec2 uv = UV-.5;
	uv.y *= -1.;
	uv *= 1.5;
    uv = floor(uv/pix)*pix;
	vec4 col = vec4(0.);
	for (float i = 0.; i < 8.; ++i)
    {
        float f = i/8.-.5;
        col = dunno(uv+vec2(f, 0.), i, col);
    
    }
	COLOR = col;

	// Place fragment code here.
}
