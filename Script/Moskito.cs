using Godot;
using System;

public class Moskito : Area2D
{
    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
