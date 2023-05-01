using Godot;
using System;

public partial class Indicator : Node2D
{
	[Export]
	public Node2D planet;
	[Export]
	public Node2D rocket;

	private Vector2 direction;

	public override void _Process(double delta)
	{
		direction = (planet.GlobalPosition - rocket.GlobalPosition).Normalized();
		this.GlobalPosition = rocket.GlobalPosition + direction * 100f;
		this.GlobalRotation = (float)Math.Atan2(direction.Y, direction.X) + 0.5f * (float)Math.PI;
	}
}
