using Godot;
using System;

public class SoundBullet : Area2D
{

    private int _speed = 750;
    private float _ttl = 0.75f;
    private float _timer = 0f;

    public override void _PhysicsProcess(float delta)
    {
        if (_ttl <= _timer)
        {
            QueueFree();
        }
        Position += Transform.x * _speed * delta;
        _timer += delta;
    }
    public void OnSoundBulletAreaEntered(Area2D area)
    {
        area.QueueFree();
    }
}
