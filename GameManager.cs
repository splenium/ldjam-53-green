using Godot;
using System;

public partial class GameManager : Node
{
	// All these attribute are for Mission:
	private string _missionName;
	private int _missionReward;
	private int _missionPlanetTarget;


	public int cash { get; set; }
	public int PlanetPosition { get; set; }
	public string[] PlanetNames = new string[]
	{
		"Fecaloria",
		"Odorix",
		"Etronis"
	};

	private string _planetScene = "res://scenes/game/PlanetScene.tscn";
    private string _spaceScene = "res://scenes/game/SpaceScene.tscn";

    // Called when the node enters the scene tree for the first time.

    private int _startCash = 0;
	public override void _Ready()
	{
		this.NewGame();
	}

    public void NewGame()
	{
		this.cash = _startCash;
		this.PlanetPosition = 0;
	}

	public void LoadScene(string scenePath)
	{
		GetTree().ChangeSceneToFile(scenePath);
	}

	public void LoadMission(Mission mission)
	{
		_missionName = mission.Name;
		_missionPlanetTarget = mission.planetTarget;
		_missionReward = mission.reward;
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

	// ALL METHOD FOR MISSION CONTROL

    public bool MissionInProgress()
    {
        return _missionName != null;
    }

    public int MissionReward()
    {
        return _missionReward;
    }

    public int MissionPlanetTarget()
    {
        return _missionPlanetTarget;
    }

    public void MissionSucces()
	{
		if(MissionInProgress())
		{
			this.cash += _missionReward;
			ClearMission();
		}
	}

	private void ClearMission()
	{
		_missionName = null;
	}

	public void AbortTheMission()
	{
		ClearMission();
	}
}
