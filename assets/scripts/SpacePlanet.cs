using Godot;
using System;

public partial class SpacePlanet : Node2D
{
	[Export]
	public int PlanetId;

	[Export]
	private Texture2D PlanetTexture;

	[Export]
	private Label PlanetNameLabel;
	[Export]
	private Label LandingLabel;
	[Export]
	private Sprite2D PlanetSprite;
	[Export]
	public Node2D ExitShipPosition;

	private GameManager _gameManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _gameManager = GetNode<GameManager>("/root/GameManager");
		LandingLabel.Visible = false;
		PlanetSprite.Texture = PlanetTexture;

        if(PlanetNameLabel != null)
		{
			PlanetNameLabel.Text = "[" + _gameManager.PlanetNames[PlanetId] + "]";
		}
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustReleased("mission_one") && LandingLabel.Visible)
		{
			_gameManager.LandingOn(PlanetId);
		}
	}

	private bool isFinalPlanet()
	{
		return _gameManager.lastMissionPlanet == PlanetId;

    }

	private void SetLandingLabel(bool visible, Node2D node)
	{
		CharacterBody2D c = node as CharacterBody2D;
		bool finalMissionActive = _gameManager.IsFinalMission();
		if(c != null && ((!finalMissionActive && !isFinalPlanet()) || (finalMissionActive && isFinalPlanet()))) // Final Conditon for planet entrance
		{
			LandingLabel.Visible = visible;
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		this.SetLandingLabel(true, body);
	}

	private void OnBodyExited(Node2D body)
	{
		this.SetLandingLabel(false, body);
	}
}
