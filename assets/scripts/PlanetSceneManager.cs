using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;

public partial class PlanetSceneManager : Node2D
{
	[Export]
	private Node PlanetsContainer;
	[Export]
	private Label PlanetLabelName;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager gm = GetNode<GameManager>("/root/GameManager");
		List<Node> childToRemove = new();
		Godot.Collections.Array<Node> planets = PlanetsContainer.GetChildren();

		long thePlanet = gm.PlanetPosition;

        for (int i = 0; i < planets.Count; i++)
		{
			if(i != thePlanet)
			{
				childToRemove.Add(planets[i]);
			}
		}

		foreach(Node toRemove in childToRemove) {
			PlanetsContainer.RemoveChild(toRemove);
		}

		if(PlanetLabelName != null)
		{
			PlanetLabelName.Text = "🪐 " + gm.PlanetNames[thePlanet]; // the special char is saturn icon :)
		}
	}

    /*public override void _Process(double delta)
    {
        if(Input.IsActionJustReleased("echap"))
		{
			GetTree().ReloadCurrentScene();
		}
    }*/


}
