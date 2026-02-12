using Godot;
using GFramework.Core.Abstractions.controller;
using GFramework.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;


[ContextAware]
[Log]
public partial class AI_TRPG :Control,IController
{
	private VBoxContainer ChatBox => GetNode<VBoxContainer>("%对话框");
	private TextEdit InputBox => GetNode<TextEdit>("%输入文本框");
	private Button SendButton => GetNode<Button>("%发送按钮");


	/// <summary>
	/// 节点准备就绪时的回调方法
	/// 在节点添加到场景树后调用
	/// </summary>
	public override void _Ready()
	{
		SendButton.Pressed += OnSendButtonPressed;
	}


	/// <summary>
	/// 处理发送消息时的逻辑，可以被发送按钮或回车键触发
	/// </summary>
	public void SendMessage()
	{
		
	}

	/// <summary>
	/// 发送按钮按下时的回调方法
	/// </summary>
	private void OnSendButtonPressed()
	{
		SendMessage();
	}
}


