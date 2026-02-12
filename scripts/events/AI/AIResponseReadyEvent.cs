namespace GFrameworkGodotTemplate.scripts.events.AI;

/// <summary>
/// AI回复准备就绪事件
/// 当AI生成回复后，通过此事件通知Controller显示结果
/// </summary>
public class AIResponseReadyEvent
{
    public string AIResponse { get; }
    
    public AIResponseReadyEvent(string aiResponse)
    {
        AIResponse = aiResponse;
    }
}


