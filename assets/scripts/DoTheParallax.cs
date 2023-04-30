using Godot;
using System;

public partial class DoTheParallax : Node2D
{
	[Export]
	private float Speed = 0.1f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float diffX = GetViewport().GetCamera2D().GlobalPosition.X * Speed;

		this.Position = new Vector2(diffX, this.Position.Y);
    }
}
