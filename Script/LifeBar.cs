using Godot;
using System;

public class LifeBar : Container
{

    private int _max = 5;
    private int _currentValue = 5;

    public override void _Ready()
    {
        _max *= 10;
        var textureProgress = GetNode<TextureProgress>("TextureProgress");
        textureProgress.MaxValue = _max;
        textureProgress.Value = _max;
        //AnimateValue(_currentValue, _currentValue);
    }

    // Signal callback
    public void OnComputerHPChanged(int hp)
    {
        AnimateValue(_currentValue, hp);
    }

    private void AnimateValue(int start, int end)
    {
        end *= 10;
        var tween = GetNode<Tween>("Tween");
        var textureProgress = GetNode<TextureProgress>("TextureProgress");
        tween.InterpolateProperty(textureProgress, "value", start, end, 0.5f, Tween.TransitionType.Linear, Tween.EaseType.Out);
        tween.Start();
        _currentValue = end;
        textureProgress.TintProgress = Color.Color8(255, Convert.ToByte(_currentValue * 5), 0);
        if (_currentValue == 0)
            GetNode<AnimationPlayer>("AnimationPlayer").Play("shake");
    }
}
