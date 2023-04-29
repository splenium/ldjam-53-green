using Godot;
using System;

public partial class PersoPrefab : CharacterBody2D
{
	private float moveSpeed = 200f;
	[Export]
	private CharacterBody2D CharacterBody;
	[Export]
	private AnimatedSprite2D AnimatedSprite;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Vector2 move = Vector2.Zero;
		if (Input.IsActionPressed("right"))
		{
			AnimatedSprite.Scale = new Vector2(1, 1);
			move.X = this.moveSpeed;
		}
		if (Input.IsActionPressed("left"))
		{
			AnimatedSprite.Scale = new Vector2(-1, 1);
			move.X = -this.moveSpeed;
		}
		if (move.Length() > 0.1)
			AnimatedSprite.Play("Walk");
		else
			AnimatedSprite.Play("Idle");

		CharacterBody.Velocity = move;
		CharacterBody.MoveAndSlide();
	}
}
