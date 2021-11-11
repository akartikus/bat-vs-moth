using Godot;
using System;

public class Bat : KinematicBody2D
{
    [Export] private int _speed = 400;
    [Export] private PackedScene Bullet;
    [Signal] public delegate void OnEat();
    [Signal] public delegate void UpdateCombo(int point);
    public const int ComboPointGoal = 3;

    private Vector2 _screenSize;
    private int _comboPoint = 0;
    private bool _isComboReady;

    public override void _Ready()
    {
        _screenSize = GetViewport().Size;
    }

    public void OnMouthAreaEntered(Area2D area)
    {
        if (area.GetType() == typeof(Moth))
        {
            EmitSignal(nameof(OnEat));
        }
        else if (area.GetType() == typeof(Moskito))
        {
            if (_isComboReady)
            {
                return;
            }
            //EmitSignal(nameof(UpdateCombo), _comboPoint);
            _comboPoint += 1;
            GetNode<BatCombo>("Combo").EarnCP(_comboPoint);

            if (_comboPoint == ComboPointGoal)
            {
                _isComboReady = true;
            }
        }

        area.QueueFree();
    }

    public override void _Process(float delta)
    {
        var velocity = new Vector2();

        if (Input.IsActionPressed("right"))
        {
            velocity.x += 1;
        }

        if (Input.IsActionPressed("left"))
        {
            velocity.x -= 1;
        }

        if (Input.IsActionPressed("down"))
        {
            velocity.y += 1;
        }

        if (Input.IsActionPressed("up"))
        {
            velocity.y -= 1;
        }

        if (Input.IsActionJustPressed("shoot"))
        {
            if (_isComboReady)
            {
                Shoot();
                _isComboReady = false;
                _comboPoint = 0;
                GetNode<BatCombo>("Combo").InitializeCP();
                //EmitSignal("UpdateCombo", _comboPoint);
            }
        }
        //var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        var sprite = GetNode<Sprite>("Sprite");

        // FIXME :  Use a well oriented asset 
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * _speed;
            // animatedSprite.Play();
        }

        if (velocity.x != 0)
        {
            var flipH = (velocity.x > 0) ? -1 : 1;
            Scale = new Vector2(flipH, Scale.y);
        }
        else if (velocity.y != 0)
        {
            var flipV = (velocity.y > 0) ? -1 : 1;
            Scale = new Vector2(Scale.x, flipV);
        }

        Position += velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, _screenSize.x),
            y: Mathf.Clamp(Position.y, 0, _screenSize.y)
        );
    }

    public async void Shoot()
    {
        var waveTimer = GetNode<Timer>("WaveTimer");
        waveTimer.Start();

        for (int wave = 0; wave < 5; wave++)
        {
            if (wave != 0) await ToSignal(waveTimer, "timeout");
            var nbBullets = 10;
            var rotation = -30;
            var rotationStep = 5;

            for (int i = 0; i < nbBullets; i++)
            {
                var b = (SoundBullet)Bullet.Instance();
                b.Position = Position;
                b.Scale = -Scale;
                rotation = rotation + rotationStep;
                b.RotationDegrees = rotation;

                GetNode<Node2D>("../").AddChild(b);
            }
            waveTimer.Start();
        }
    }

}
