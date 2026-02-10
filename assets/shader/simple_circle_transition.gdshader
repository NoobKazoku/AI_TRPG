shader_type canvas_item;

// ========================
// 标准输入（SceneTransitionManager 注入）
// ========================
uniform sampler2D from_tex;
uniform sampler2D to_tex;
uniform float progress : hint_range(0.0, 1.0);

// ========================
// 屏幕参数
// ========================
uniform vec2 resolution;

// ========================
// Circle Reveal 参数
// ========================
uniform vec2 circle_position = vec2(0.5, 0.5);
uniform float smoothness : hint_range(0.0, 0.1) = 0.03;

// ========================
// Fragment
// ========================
void fragment() {
	vec2 uv = UV;

	// 修正宽高比
	float r = resolution.y / resolution.x;

	vec2 position = circle_position * vec2(r, 1.0);
	vec2 new_uv = uv * vec2(r, 1.0);

	// 找最远角
	float far_x = float(circle_position.x < 0.5) * r;
	float far_y = float(circle_position.y < 0.5);
	float r_max = distance(position, vec2(far_x, far_y));

	// progress：0 → 1（从遮罩到完全显示）
	float current = r_max * (1.0 - progress);
	float edge = current + smoothness;

	float mask = smoothstep(current, edge, distance(new_uv, position));

	vec4 from_col = texture(from_tex, uv);
	vec4 to_col   = texture(to_tex, uv);

	// mask = 0 → from
	// mask = 1 → to
	vec4 mixed = mix(from_col, to_col, mask);

	// 背景色兜底
	COLOR = mixed;
}
