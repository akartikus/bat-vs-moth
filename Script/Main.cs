using Godot;
using System;
using System.Collections.Generic;

public class Main : Node2D
{
    [Export] private PackedScene Moth;
    [Export] private PackedScene Moskito;

    private Random _random = new Random();
    private Label _scoreLabel;
    private Label _hpLabel;
    private Label _comboLabel;
    private List<Vector2> _windowsPosition;

    private int _score;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("Control/ColorRect/VBoxContainer/score");
        _hpLabel = GetNode<Label>("Control/ColorRect/VBoxContainer/hp");
        _comboLabel = GetNode<Label>("Control/ColorRect/VBoxContainer/combo");
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
        var global = GetNode<Global>("/root/Global");
        global.Score = _score;
        global.GotoScene("res://Scenes/GameOver.tscn");
        //GetTree().ChangeScene("res://Scenes/GameOver.tscn");
    }
    public void OnMothTimerTimeout()
    {
        spawnMoth();
        GetNode<Timer>("MothTimer").WaitTime -= 0.01f;
    }

    public void OnMoskitoTimerTimeout()
    {
        spawnMoskito();
    }

    public void OnStartTimerTimeout()
    {
        GetNode<Timer>("MothTimer").Start();
        GetNode<Timer>("MoskitoTimer").Start();
    }

    public void NewGame()
    {
        _score = 0;
        GetNode<Timer>("StartTimer").Start();
    }

    public void OnBatPlayerUpdateCombo(int point)
    {
        if (point < 3)
        {
            _comboLabel.Text = "Combo point : " + point;
        }
        else
        {
            _comboLabel.Text = "Combo ready!!!!!!";
        }
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

    private void spawnMoskito()
    {
        var screenSize = GetViewport().GetVisibleRect().Size;
        var posX = RandRange(0, (int)screenSize.x);
        var posY = RandRange(0, (int)screenSize.y);

        var moskitoInstance = (Moskito)Moskito.Instance();
        moskitoInstance.Position = new Vector2(posX, posY);
        GetNode<Node2D>("Insects").AddChild(moskitoInstance);
    }

    private float RandRange(float min, float max)
    {
        return (float)_random.NextDouble() * (max - min) + min;
    }

}
