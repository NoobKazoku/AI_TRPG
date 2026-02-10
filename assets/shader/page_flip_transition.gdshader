shader_type canvas_item;

uniform sampler2D from_tex;
uniform sampler2D to_tex;

uniform float progress : hint_range(0.0, 1.0) = 0.0;
uniform float meltiness : hint_range(0.0, 16.0) = 1.0;

float pseudo_rand(float x) {
    return fract(x * 2048103.0 + cos(x * 1912.0));
}

void fragment() {
    vec2 uv = UV;

    // -------- 翻页位移 --------
    float y_factor = max(uv.y, 0.05);
    uv.y -= progress / y_factor;

    float column = UV.x - mod(UV.x, TEXTURE_PIXEL_SIZE.x);
    uv.y -= progress * meltiness * pseudo_rand(column);

    vec2 melt_uv = clamp(uv, vec2(0.0), vec2(1.0));

    vec4 from_color = texture(from_tex, melt_uv);
    vec4 to_color   = texture(to_tex, UV);

    // 超出范围：from 消失
    float mask = step(0.0, uv.y);

    // 👉 核心：翻页的地方显示 to_tex
    COLOR = mix(to_color, from_color, mask);
}
