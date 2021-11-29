using Godot;
using System;

public class ComboBar : Container
{
    private int _max = 3;
    private int _currentValue = 0;

    public override void _Ready()
    {
        _max *= 10;
        AnimateValue(_currentValue, _currentValue);
    }
    public void OnPlayerCPChanged(int newCP)
    {
        AnimateValue(_currentValue, newCP);
    }

    private void AnimateValue(int start, int end)
    {
        end *= 10;
        var tween = GetNode<Tween>("Tween");
        var textureProgress = GetNode<TextureProgress>("TextureProgress");
        var transitionType = end == _max ? Tween.TransitionType.Bounce : Tween.TransitionType.Linear;
        tween.InterpolateProperty(textureProgress, "value", start, end, 0.5f, transitionType, Tween.EaseType.Out);
        tween.Start();
        _currentValue = end;
        if (_currentValue == _max)
        {
            GetNode<AnimationPlayer>("AnimationPlayer").Play("shake");
        }
        else
        {
            textureProgress.RectRotation = 0;
            GetNode<AnimationPlayer>("AnimationPlayer").Stop();
        }
    }
}
