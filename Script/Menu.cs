using Godot;
using System;

public class Menu : Control
{
    public void OnReplayPressed()
    {
        var global = GetNode<Global>("/root/Global");
        global.GotoScene("res://Scenes/Main.tscn");
    }

    public void OnQuitPressed()
    {
        GetTree().Quit();
    }
}
