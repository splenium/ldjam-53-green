using Godot;
using System;
using System.Linq;

public partial class AsteroidsManager : Node
{
    private Vector2[] _offSets;

    [Export]
    private PackedScene[] _asteroidPrefabs;

    [Export]
    private Timer spawnTimer;

    [Export]
    private CharacterBody2D playerSpaceships;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _offSets = new Vector2[] { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1) };
    }

    void _on_asteroids_timer_timeout()
    {
        var instance = _asteroidPrefabs[(int)GD.RandRange(0, _asteroidPrefabs.Length - 1)].Instantiate() as Node2D;
        AddChild(instance);

        Vector2 spawnPosition = playerSpaceships.GlobalPosition + _offSets[(int)GD.RandRange(0, _offSets.Length - 1)] * 645;
        instance.GlobalPosition = spawnPosition;

        spawnTimer.Start(spawnTimer.WaitTime);
    }
}
