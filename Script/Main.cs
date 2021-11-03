using Godot;
using System;

public class Main : Node2D
{
    [Export] private PackedScene Moth;

    private int _score;
    private Random _random = new Random();

    public override void _Ready()
    {

    }

    private float RangeRange(float min, float max)
    {
        return (float)_random.NextDouble() * (max - min) + min;
    }
}
