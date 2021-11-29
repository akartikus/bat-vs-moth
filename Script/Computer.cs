using Godot;
using System;

public class Computer : Area2D
{
    [Export] private int _hpMax = 5;
    [Signal] public delegate void OnDie();
    [Signal] public delegate void HPChanged(int hp);
    private int _hp = 5;

    public override void _Ready()
    {
        _hp = _hpMax;
    }

    public void OnComputerAreaEntered(Area2D area)
    {
        if (_hp > 0)
        {
            _hp--;
            EmitSignal(nameof(HPChanged), _hp);
            GD.Print("HP : " + _hp);
        }
        else
        {
            EmitSignal("OnDie");
        }
    }
}
