using Godot;
using System;

namespace Alice.UI;

public partial class DialogueTextBox : Node
{
	private RichTextLabel _richLabel;

	public override void _Ready()
	{
		_richLabel = GetNode<RichTextLabel>("RichTextLabel");
	}

	public void ShowText(string text)
	{
		_richLabel.Text = text;
	}
}
