using Godot;
using System;
using System.Linq;

public partial class AsteroidsManager : Node
{
    private PackedScene _asteroidPrefabs;
    private Vector2[] _offSets;

    private PackedScene[] _asteroidsPrefabs;
    [Export]
    private Timer spawnTimer;

    [Export]
    private CharacterBody2D playerSpaceships;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _asteroidPrefabs = GD.Load("res://assets/prefabs/asteroid.tscn") as PackedScene;
        _offSets = new Vector2[] { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1) };
    }

    void _on_asteroids_timer_timeout()
    {
        var instance = _asteroidPrefabs.Instantiate() as Node2D;
        AddChild(instance);

        Vector2 spawnPosition = playerSpaceships.GlobalPosition + _offSets[(int)GD.RandRange(0, _offSets.Count()-1)] * 645;
        instance.GlobalPosition = spawnPosition;

        spawnTimer.Start(spawnTimer.WaitTime);
    }
}
