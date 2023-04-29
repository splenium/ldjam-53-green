using Godot;
using System;

public partial class AnimateDrone : Sprite2D
{
    float _origiY;
    double _time;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _origiY = this.Position.Y;

        _time = 0.0f;
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        this.Position = new Vector2(this.Position.X, (float)(_origiY + Math.Sin(_time*2.0)));
        _time += delta;
    }
}
