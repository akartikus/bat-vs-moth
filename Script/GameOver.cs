using Godot;
using System;

public class GameOver : Control
{
    public override void _Ready()
    {
        var start = 0;
        var tween = GetNode<Tween>("Tween");
        var score = GetNode<Global>("/root/Global").Score;
        tween.InterpolateMethod(this, "UpdateScoreText", start, score, 1.5f, Tween.TransitionType.Linear, Tween.EaseType.Out);
        tween.Start();
    }

    private void UpdateScoreText(int value)
    {
        GD.Print("call!!");
        var score = GetNode<Label>("ColorRect/VBoxContainer/Score");
        score.Text = value + "";
    }
    public void OnReplayPressed()
    {
        GetTree().ChangeScene("res://Scenes/Main.tscn");
    }
}
