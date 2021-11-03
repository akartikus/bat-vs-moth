using Godot;
using System;

public class Moth : RigidBody2D
{

    [Export] private int _minSpeed = 150;
    [Export] private int _maxSpeed = 300;

    public override void _Ready()
    {
        
        base._Ready();
    }
}
