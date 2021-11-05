using Godot;
using System;

public class Moth : Area2D
{

    [Export] private int _minSpeed = 50;
    [Export] private int _maxSpeed = 150;

    private Random _random = new Random();

    private Vector2 _destination;
    public Vector2 Destination
    {
        get { return _destination; }
        set { _destination = value; }
    }

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(float delta)
    {
        Position = Position.MoveToward(Destination, RandRange(_minSpeed, _maxSpeed) * delta);
    }

    private float RandRange(float min, float max)
    {
        return (float)_random.NextDouble() * (max - min) + min;
    }
}
