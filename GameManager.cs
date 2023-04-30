using Godot;
using System;

public partial class GameManager : Node
{

	public Mission SelectedMission { get; set; }
	public int cash { get; set; }
	public int PlanetPosition { get; set; }

	// Called when the node enters the scene tree for the first time.

	private int _startCash = 40;
	public override void _Ready()
	{
		this.newGame();
	}

	public void newGame()
	{
		this.SelectedMission = null;
		this.cash = _startCash;
		this.PlanetPosition = 0;
	}

	public void LoadScene(string scenePath)
	{
		GetTree().ChangeSceneToFile(scenePath);
		//GetTree().ReloadCurrentScene();
	}

	public void LoadMission(Mission mission)
	{
		this.SelectedMission = mission;
		// this.LoadScene("res://scenes/menu.tscn");

	}

	public void MissionSucces()
	{
		if(SelectedMission != null)
		{
			this.cash += this.SelectedMission.reward;
			this.SelectedMission = null;
		}
	}
}
