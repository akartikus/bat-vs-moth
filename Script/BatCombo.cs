using Godot;
using System;

public class BatCombo : Node2D
{
    public const int CPMax = 3;

    //TODO calculate cp in this class, add signal on max combo earn, bat subscribe to batcombo state

    [Signal] public delegate void CPChanged(int cp);

    public override void _Ready()
    {
        EmitSignal(nameof(CPChanged), 0);
    }

    public void EarnCP(int amount)
    {
        EmitSignal(nameof(CPChanged), Math.Min(CPMax, amount));
    }

    public void InitializeCP()
    {
        EmitSignal(nameof(CPChanged), 0);
    }
}
