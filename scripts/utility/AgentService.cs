using Godot;
using GFramework.Core.Abstractions.controller;
using GFramework.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;


namespace GFrameworkGodotTemplate.scripts.utility;

[ContextAware]
[Log]
public partial class AgentService :IAgentService
{
	public string ProcessMessage(string PlayerMessage)
	{
		// 记录日志：程序收到了用户的消息
		_log.Debug("系统收到用户信息消息...");
        _log.Info($"用户消息: {PlayerMessage}");

        
        // TODO: 这里后续会调用DeepSeek API
        // 现在先用简单的模拟回复
        string aiResponse = $"AI收到了你的消息: \"{PlayerMessage}\"";
        
        // 记录日志：程序生成了AI回复
		_log.Debug("系统生成AI回复消息...");
        _log.Info($"AI消息: {aiResponse}");
        
        // 返回AI回复给调用者
        return aiResponse;
	}
}


