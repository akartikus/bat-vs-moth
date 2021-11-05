using Godot;
using System;

public class Main : Node2D
{
    [Export] private PackedScene Moth;

    private Random _random = new Random();

    private int _score;

    public override void _Ready()
    {
        NewGame();
    }
    public void OnBatPlayerEat()
    {
        _score++;
        GD.Print("score : " + _score);
    }

    public void OnMothTimerTimeout()
    {
        var mobSpawnLocation = GetNode<PathFollow2D>("MothPath/MothSpawnLocation");
        mobSpawnLocation.Offset = _random.Next();

        var mothInstance = (Moth)Moth.Instance();

        mothInstance.Position = mobSpawnLocation.Position;

        var computerPosition = GetNode<Area2D>("Computer").Position;
        mothInstance.LookAt(computerPosition);

        mothInstance.Destination = computerPosition;
        AddChild(mothInstance);

    }
    public void OnStartTimerTimeout()
    {
        GetNode<Timer>("MothTimer").Start();
    }

    public void NewGame()
    {
        _score = 0;
        GetNode<Timer>("StartTimer").Start();
    }


}
