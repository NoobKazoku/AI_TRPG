using Godot;
using GFramework.Core.Abstractions.controller;
using GFramework.SourceGenerators.Abstractions.logging;
using GFramework.SourceGenerators.Abstractions.rule;
using GFrameworkGodotTemplate.scripts.events.AI;
using GFrameworkGodotTemplate.scripts.command.AI;
using GFramework.Core.extensions;

namespace AITRPG;
[ContextAware]
[Log]
public partial class AI_TRPG :Control,IController
{
	private ScrollContainer ChatBox => GetNode<ScrollContainer>("%对话框");
	private TextEdit InputBox => GetNode<TextEdit>("%输入文本框");
	private Button SendButton => GetNode<Button>("%发送按钮");



	// 加载用户消息气泡场景
	public PackedScene playerTextBoxScene = ResourceLoader.Load<PackedScene>("res://scenes/text_box/player_text_box.tscn");
	// 加载AI消息气泡场景
	public PackedScene AITextBoxScene = ResourceLoader.Load<PackedScene>("res://scenes/text_box/AI_text_box.tscn");
	/// <summary>
	/// 节点准备就绪时的回调方法
	/// 在节点添加到场景树后调用
	/// </summary>
	public override void _Ready()
	{
		SendButton.Pressed += OnSendButtonPressed;

		// 监听AI回复事件
		this.RegisterEvent<AIResponseReadyEvent>(OnAIResponseReady);
	}

	/// <summary>
	/// 处理用户发送消息时的逻辑，可以被发送按钮或回车键触发
	/// </summary>
	public void PlayerSendMessage()
	{
		// 如果输入框为空，则不发送消息
		if (string.IsNullOrWhiteSpace(InputBox.Text))return;

		// 实例化用户消息气泡场景
		var playerTextBoxInstance = playerTextBoxScene.Instantiate();

		// 设置用户消息气泡的文本为输入框中的内容
		var dialogTextLabel = playerTextBoxInstance.GetNode<Label>("%对话文本");
		dialogTextLabel.Text = InputBox.Text;

		// 将实例添加到聊天框中
		ChatBox.AddChild(playerTextBoxInstance);

		// 滚动到底部
		ChatBox.ScrollVertical = (int)ChatBox.GetVScrollBar().MaxValue;

		// 保存用户消息用于发送Command
		string userMessage = InputBox.Text;
		
		// 清空输入框
		InputBox.Text = "";
		
		// 发送AI对话Command
		this.SendCommand(new AIChatCommand(new AIChatCommandInput(userMessage)));
		
		// 清空输入框
		InputBox.Text = "";

	}

	/// <summary>
	/// 发送按钮按下时的回调方法
	/// </summary>
	private void OnSendButtonPressed()
	{
		PlayerSendMessage();
	}

	/// <summary>
	/// 处理AI回复事件
	/// </summary>
	private void OnAIResponseReady(AIResponseReadyEvent e)
	{
		// 显示AI回复
		var aiTextBoxInstance = AITextBoxScene.Instantiate();
		var dialogTextLabel = aiTextBoxInstance.GetNode<Label>("%对话文本");
		dialogTextLabel.Text = e.AIResponse;
		ChatBox.AddChild(aiTextBoxInstance);
		
		// 滚动到底部
		ChatBox.ScrollVertical = (int)ChatBox.GetVScrollBar().MaxValue;
	}
}


