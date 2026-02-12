using Godot;
using GFramework.Core.Abstractions.controller;
using GFramework.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;
using GFramework.Core.Abstractions.utility;

namespace GFrameworkGodotTemplate.scripts.utility;
/// <summary>
/// AI代理服务接口 - 负责处理AI对话逻辑
/// </summary>
public interface IAgentService : IUtility
{
	/// 用户消息
    string ProcessMessage(string userMessage);
}


