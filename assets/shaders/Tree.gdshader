shader_type canvas_item;

uniform vec4 _barkColor;
uniform vec4 _leafColor;
uniform float _branchAngleFactor;
uniform float _seed;

#define sat(a) clamp(a, 0., 1.)

mat2 rot(float angle) {
    float cs = cos(angle);
	float ss = sin(angle); 
    return mat2(vec2(cs, -ss), vec2(ss, cs));
}

float _sqr(vec2 uv, vec2 s)
{
    vec2 l = abs(uv)-s;
    return max(l.x, l.y);
}
vec3 rdrtree(vec2 uv, vec3 col)
{
  float sharp = 40000.;
  uv.x+= sin(uv.y*5.+uv.x*2.+TIME+11.33*_seed)*.02*sat(uv.y);
  vec2 p = uv;
  for (float i = 0.;i<6.;++i)
  {
    float off = mix(.3,.05,i/7.);
    p+= vec2(0.,-off*.5);
    p*=rot(_branchAngleFactor*sat(i));
    p+= vec2(0.,-off*.5);
    p.x+= sin(p.y*20.)*.02;
    p.x = abs(p.x);
    vec2 pos = p;

    float shape = _sqr(pos, vec2(.03,off*.45));
    shape -=.012
    ;//-texture(iChannel0,uv*.15).x*.02;
    if(i<4.5)
      col = mix(col, _barkColor.xyz,1.-sat(shape*sharp));
    else
    {
      float cir = length(p)-.05;
      col = mix(col, _leafColor.xyz,
        1.-sat(cir*sharp));

    }
  }
  return col;
}

void fragment() {
	float pix = .01;
	vec2 uv = UV-.5;
     uv = floor(uv/pix)*pix;
	uv .y -= .4;
	vec3 col = vec3(0.,1.,0.);
	 col = rdrtree(uv*1.7*vec2(1.,-1.), col);
	COLOR = vec4(col,float(col.y < 0.5));
}
