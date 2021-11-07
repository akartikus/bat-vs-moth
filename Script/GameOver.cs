using Godot;
using System;

public class GameOver : Control
{

    public void OnReplayPressed()
    {
        GetTree().ChangeScene("res://Scenes/Main.tscn");
    }
}
