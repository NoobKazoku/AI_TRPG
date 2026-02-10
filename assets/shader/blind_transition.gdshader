shader_type canvas_item;

uniform sampler2D from_tex;
uniform sampler2D to_tex;
uniform float progress : hint_range(0.0, 1.0) = 0.0;
uniform int row_count : hint_range(2, 32) = 8;
uniform bool top_to_bottom = true;

// 百叶窗模式
uniform int mode = 0; // 0=顺序, 1=交错, 2=随机, 3=向中心, 4=向两边

void fragment() {
    float row_height = 1.0 / float(row_count);
    int current_row = int(UV.y / row_height);
    
    // ========== 计算行进度（不同模式） ==========
    float row_progress = 0.0;
    
    if (mode == 0) {
        // 顺序：从上到下/从下到上
        if (top_to_bottom) {
            row_progress = progress * float(row_count) - float(current_row);
        } else {
            row_progress = progress * float(row_count) - float(row_count - 1 - current_row);
        }
    }
    else if (mode == 1) {
        // 交错：奇偶行交替
        float offset = mod(float(current_row), 2.0) * 0.5;
        row_progress = (progress - offset) * 2.0;
    }
    else if (mode == 2) {
        // 随机：每行随机延迟
        float random_offset = fract(sin(float(current_row) * 12.9898) * 43758.5453);
        row_progress = (progress - random_offset * 0.5) * 2.0;
    }
    else if (mode == 3) {
        // 向中心：从上下两边向中间
        int middle = row_count / 2;
        int distance_from_middle = abs(current_row - middle);
        row_progress = progress * float(row_count) - float(distance_from_middle);
    }
    else if (mode == 4) {
        // 向两边：从中间向上下两边
        int middle = row_count / 2;
        int distance_from_middle = abs(current_row - middle);
        row_progress = progress * float(row_count) - float(middle - distance_from_middle);
    }
    
    row_progress = clamp(row_progress, 0.0, 1.0);
    
    // ========== 简单混合（无旋转） ==========
    vec4 from_color = texture(from_tex, UV);
    vec4 to_color = texture(to_tex, UV);
    
    COLOR = mix(from_color, to_color, row_progress);
}