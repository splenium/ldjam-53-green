using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private float moveSpeed = 200f;
	[Export]
	private float gravity = 500f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 move = Vector2.Zero;
		if(Input.IsActionPressed("right"))
		{
			move.X = this.moveSpeed;
		}
		if(Input.IsActionPressed("left"))
		{
			move.X = -this.moveSpeed;
		}
		move.Y += gravity;
		this.Velocity = move;
		this.MoveAndSlide();
		//this.MoveAndCollide(move * ((float)delta));
	}
}
