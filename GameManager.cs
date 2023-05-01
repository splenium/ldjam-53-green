using Godot;
using System;

public partial class GameManager : Node
{

	public Mission SelectedMission { get; set; }
	public int cash { get; set; }
	public int PlanetPosition { get; set; }
	public string[] PlanetNames = new string[]
	{
		"Fartopia",
		"Odorix"
	};

	private string _planetScene = "res://scenes/game/PlanetScene.tscn";
    private string _spaceScene = "res://scenes/game/SpaceScene.tscn";

    // Called when the node enters the scene tree for the first time.

    private int _startCash = 100;
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
	}

	public void LoadMission(Mission mission)
	{
		this.SelectedMission = mission;

	}

	public void LandingOn(int planetId)
	{
		this.PlanetPosition = planetId;
		this.LoadScene(_planetScene);
	}

	public void GoInSpace()
	{
		LoadScene(_spaceScene);
	}

	public void Pay(int cost)
	{
		this.cash -= cost;
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
