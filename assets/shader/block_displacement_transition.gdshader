shader_type canvas_item;

/* 分块数量 */
uniform vec2 blocks = vec2(10.0, 6.0);

/* 0.0 → 1.0 的切换进度 */
uniform float progress : hint_range(0.0, 1.0);

/* 块位移强度 */
uniform float move_strength = 0.15;

void fragment() {
    // 原始 UV
    vec2 uv = UV;

    // 每个块的尺寸
    vec2 block_size = 1.0 / blocks;

    // 当前像素属于哪个块
    vec2 block_id = floor(uv / block_size);

    // 当前块内的局部 UV（0~1）
    vec2 local_uv = fract(uv / block_size);

    // 给每个块一个稳定的随机值
    float rnd = fract(
        sin(dot(block_id, vec2(12.9898, 78.233))) * 43758.5453
    );

    // 每个块的延迟
    float block_progress = clamp(
        (progress - rnd * 0.3) / (1.0 - 0.3),
        0.0,
        1.0
    );

    // 块位移方向（向外炸开）
    vec2 offset_dir = normalize(block_id - blocks * 0.5 + 0.001);

    // 位移
    vec2 offset = offset_dir * move_strength * block_progress;

    // 关键点：采样原画面内容
    vec2 sample_uv = uv - offset;

    // 防止采样越界导致黑色
    sample_uv = clamp(sample_uv, vec2(0.0), vec2(1.0));

    COLOR = texture(TEXTURE, sample_uv);

    // 可选：淡出
    COLOR.a *= 1.0 - block_progress;
}
