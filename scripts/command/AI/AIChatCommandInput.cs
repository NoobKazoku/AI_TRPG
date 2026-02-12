namespace GFrameworkGodotTemplate.scripts.command.AI;

/// <summary>
/// AI对话命令的输入参数
/// </summary>
public class AIChatCommandInput
{
    public string PlayerMessage { get; }
    
    public AIChatCommandInput(string playerMessage)
    {
        PlayerMessage = playerMessage;
    }
}


