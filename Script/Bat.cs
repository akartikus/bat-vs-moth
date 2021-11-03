using Godot;
using System;

public class Bat : Area2D
{
    [Export] private int _speed = 400;
    private Vector2 _screenSize;

    public override void _Ready()
    {
        _screenSize = GetViewport().Size;
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
            sprite.FlipV = false;
            sprite.FlipH = !(velocity.x < 0);
        }
        else if (velocity.y != 0)
        {
            sprite.FlipV = (velocity.y > 0);
        }

 


        Position += velocity * delta;

        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, _screenSize.x),
            y: Mathf.Clamp(Position.y, 0, _screenSize.y)
        );
    }

}
