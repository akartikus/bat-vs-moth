using Godot;
using System;
using System.Collections.Generic;

public class Main : Node2D
{
    [Export] private PackedScene Moth;

    private Random _random = new Random();
    private Label _scoreLabel;
    private Label _hpLabel;
    private List<Vector2> _windowsPosition;

    private int _score;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("Control/ColorRect/VBoxContainer/score");
        _hpLabel = GetNode<Label>("Control/ColorRect/VBoxContainer/hp");
        InitializeWindowsPosition();
        NewGame();
    }
    public void OnBatPlayerEat()
    {
        _score++;
        _scoreLabel.Text = "score : " + _score;
    }

    public void OnComputerDie()
    {
        GD.Print("Game over");
        //GetTree().Paused = true;
        GD.Print("In pause");
    }
    public void OnMothTimerTimeout()
    {
        spawnMoth();
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

    private void InitializeWindowsPosition() => _windowsPosition = new List<Vector2>{
            (Vector2)GetNode<Node2D>("Computer/Window1").GlobalPosition,
            (Vector2)GetNode<Node2D>("Computer/Window2").GlobalPosition,
            (Vector2)GetNode<Node2D>("Computer/Window3").GlobalPosition,
            (Vector2) GetNode<Node2D>("Computer/Window4").GlobalPosition
        };

    private void spawnMoth()
    {
        var mobSpawnLocation = GetNode<PathFollow2D>("MothPath/MothSpawnLocation");
        mobSpawnLocation.Offset = _random.Next();

        var mothInstance = (Moth)Moth.Instance();

        mothInstance.Position = mobSpawnLocation.Position;

        var i = _random.Next(_windowsPosition.Count);

        mothInstance.LookAt(_windowsPosition[i]);

        mothInstance.Destination = _windowsPosition[i];

        GetNode<Node2D>("Insects").AddChild(mothInstance);
    }

}
