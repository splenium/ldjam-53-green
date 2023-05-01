using Godot;
using System;

public partial class SpacePlayerSpawner : Node2D
{
	[Export]
	private CharacterBody2D Player;
	[Export]
	private Node PlanetContainter;
	private GameManager _gm;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _gm = GetNode<GameManager>("/root/GameManager");
		foreach(Node planet in PlanetContainter.GetChildren())
		{
			SpacePlanet p = planet as SpacePlanet;
			if (p != null && p.PlanetId == _gm.PlanetPosition)
			{
				Player.GlobalPosition = p.ExitShipPosition.GlobalPosition;
				break;
			}
		}
    }
}
