using Godot;
using System;

public partial class MissionIndicator : HBoxContainer
{
	[Export]
	private Label planetLabel;

	private GameManager _gameManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _gameManager = GetNode<GameManager>("/root/GameManager");
    }

    public override void _Process(double delta)
    {
        if (_gameManager.MissionInProgress())
        {
            this.Visible = true;
            planetLabel.Text = _gameManager.PlanetNames[_gameManager.MissionPlanetTarget()];
        }
        else
        {
            this.Visible = false;
        }
    }
}
