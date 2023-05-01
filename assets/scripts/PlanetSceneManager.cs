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
		var planets = PlanetsContainer.GetChildren();

		long thePlanet = gm.PlanetPosition;

        foreach (Node2D planet in planets)
		{
			GroundPlanet p = planet as GroundPlanet;
			if(p != null && p.PlanetId != gm.PlanetPosition)
			{
                PlanetsContainer.RemoveChild(planet);
			}
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
