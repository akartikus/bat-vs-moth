using Godot;
using System;

public class Computer : Area2D
{
    [Export] private int _hpMax = 3;
    [Signal] public delegate void OnDie();
    private int _hp = 3;

    public override void _Ready()
    {
        _hp = _hpMax;
    }

    public void OnComputerAreaEntered(Area2D area)
    {
        if (_hp > 0)
        {
            _hp--;
            GD.Print("HP : " + _hp);
        }
        else
        {
            EmitSignal("OnDie");
        }
    }
}
