using Godot;
using System;

public partial class Follow : Node2D
{
	[Export]
	private Node2D Attached;

	private Vector2 _originalOffset;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this._originalOffset = this.Position - Attached.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Position = _originalOffset + Attached.Position;
	}
}
