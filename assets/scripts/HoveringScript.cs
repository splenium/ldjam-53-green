using Godot;
using System;

public partial class HoveringScript : Node2D
{
    [Export]
    private double Speed = 1.0;
    [Export]
    private double Amplitude = 1.0;
    [Export]
    private double Seed = 0.0;

    private double _origY;
    private double _time;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _origY = this.Position.Y;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        this.Position = new Vector2(this.Position.X, (float)((double)this.Position.Y + Amplitude*Math.Sin((_time + Seed * 11.33) * Speed)));
        _time += delta;
    }
}
