using Godot;
using System;

public partial class SpaceshipManager : Node2D
{
	private PackedScene _asteroidPrefabs;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _asteroidPrefabs = GD.Load("res://assets/asteroid.tscn") as PackedScene;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
