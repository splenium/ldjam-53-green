using Godot;
using System;


public partial class GameManager : Node
{
	public static string STRING_NULL = "[[STRING_NULL]]";
	// All these attribute are for Mission:
	private string _missionName;
	private int _missionReward;
	private int _missionPlanetTarget;


	public int cash { get; set; }
	public int PlanetPosition { get; set; }
    public bool IsDeath { get; set; }

    public string[] PlanetNames = new string[]
	{
		"Fecaloria",
		"Odorix",
		"Etronis",
        "Porcelainus"
    };

	public int WinCash = 300;

	public bool MustShowCreditsFirst;

	public string lastMissionLabel { get; } = "A mysterious package... damaged and without indication";
	public int lastMissionReward { get; } = 10000;
	public string lastMissionKey { get; } = "E";
	public int lastMissionPlanet { get; } = 3;


    private string _planetScene = "res://scenes/game/PlanetScene.tscn";
    private string _spaceScene = "res://scenes/game/SpaceScene.tscn";
    private string _menuScene = "res://scenes/game/menu.tscn";

    // Called when the node enters the scene tree for the first time.

    private int _startCash = 200;
	public override void _Ready()
	{
		this.NewGame();
	}

    public void NewGame()
	{
		this.cash = _startCash;
		this.PlanetPosition = 0;
		this.MustShowCreditsFirst = false;
		this.IsDeath = false;
		this.ClearMission();
	}

	public bool TimeForTheLastTravel()
	{
		return WinCash <= cash;
	}

	public void LoadScene(string scenePath)
	{
		GetTree().ChangeSceneToFile(scenePath);
	}

	public bool CanLandOnPlanet(int i)
	{
		bool isFinalPlanet = i == lastMissionPlanet;
		return ((!IsFinalMission() && !isFinalPlanet) || (IsFinalMission() && isFinalPlanet));
    }

	public void LoadMenu()
	{
		LoadScene(_menuScene);
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
        return _missionName != STRING_NULL;
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
			if(IsFinalMission())
			{
				MustShowCreditsFirst = true;
				LoadMenu();
			} else
			{
				this.cash += _missionReward;
				ClearMission();
			}
		}
	}

	public bool IsFinalMission()
	{
		return _missionPlanetTarget == lastMissionPlanet;
	}

	private void ClearMission()
	{
		_missionName = STRING_NULL;
	}

	public void AbortTheMission()
	{
		ClearMission();
	}
}
