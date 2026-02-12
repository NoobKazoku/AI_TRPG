using GFramework.Core.command;
using GFramework.Core.extensions;
using GFrameworkGodotTemplate.scripts.utility;
using GFrameworkGodotTemplate.scripts.events.AI;

namespace GFrameworkGodotTemplate.scripts.command.AI;

/// <summary>
/// 处理AI对话的命令
/// 负责调用AI服务并发送结果事件
/// </summary>
public class AIChatCommand : AbstractCommand
{
    private readonly AIChatCommandInput _input;
    
    public AIChatCommand(AIChatCommandInput input)
    {
        _input = input;
    }
    
    protected override void OnExecute()
    {
        // 1. 获取AI服务
        var agentService = this.GetUtility<IAgentService>();
        
        // 2. 调用AI服务处理用户消息
        string aiResponse = agentService.ProcessMessage(_input.PlayerMessage);
        
        // 3. 发送事件通知Controller显示结果
        this.SendEvent(new AIResponseReadyEvent(aiResponse));
    }
}

