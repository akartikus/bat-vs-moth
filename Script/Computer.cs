using Godot;
using System;

public class Computer : Area2D
{
    [Export] private int _hpMax = 3;
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
            // Game over
            GD.Print("Game Over!!!!");
        }
        // TODO do something with area
    }
}