using Godot;
using System;

using Alice.UI;

namespace Alice;

public partial class GameManager : Control
{
    private DialogueTextBox _dialogueTextBox;

    public override void _Ready()
    {
        _dialogueTextBox = GetNode<DialogueTextBox>("DialogueTextBox");
        _dialogueTextBox.ShowText("Welcome to the game!");
    }

    private int _counter = 0;
    public override void _Process(double delta)
    {
        // 按下回车键时显示新的对话文本
        if (Input.IsActionJustPressed("ui_accept"))
        {
            _dialogueTextBox.ShowText("You pressed the Enter key! Counter: " + _counter);
            _counter++;
        }
    }
}
