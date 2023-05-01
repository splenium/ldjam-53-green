using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

public partial class Box : Area2D
{
    [Export]
    private Control MissionSelection { get; set; }
    [Export]
    private LandscapeRocket Rocket;
    private GameManager _gameManager { get; set; }

    [Export]
    private string[] labels;
    [Export]
    private int[] rewards;
    [Export]
    private string[] keys;
    [Export]
    private int[] _planetTarget;
    public PackedScene MissionPrefabs;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        MissionPrefabs = (PackedScene) ResourceLoader.Load("res://assets/prefabs/Mission/Mission.tscn");
        MissionSelection.Visible = false;
        _gameManager = GetNode<GameManager>("/root/GameManager");

        CheckAllOk();

        if(_gameManager.TimeForTheLastTravel())
        {
            // The last mission
            SetLastMission();
        } else
        {
            // Common mission
            for (int i = 0; i < labels.Length; i++)
            {
                var missionComponent = MissionPrefabs.Instantiate();
                MissionSelection.AddChild(missionComponent);
                this.SetMission(labels[i], rewards[i], keys[i], _planetTarget[i], missionComponent);
            }
        }

    }

    private void CheckAllOk()
    {
        var n = labels.Length;
        if(n != rewards.Length || n != keys.Length || n != _planetTarget.Length)
        {
            GD.PrintErr("Impossible to parse collection labels, rewards, keys and planetTarget must have the same size !");
            GD.PrintErr("label: " + n + ", rewards: " + rewards.Length + ", keys: " + keys.Length + ", planetTarget:" + _planetTarget.Length);
            throw new Exception("Impossible to parse collection labels, rewards, keys and planetTarget must have the same size !");
        }
    }

    private void SetLastMission()
    {
        foreach(var child in MissionSelection.GetChildren())
        {
            MissionSelection.RemoveChild(child);
        }
        var missionComponent = MissionPrefabs.Instantiate();
        MissionSelection.AddChild(missionComponent);
        this.SetMission(_gameManager.lastMissionLabel, _gameManager.lastMissionReward, _gameManager.lastMissionKey, _gameManager.lastMissionPlanet, missionComponent);
    }

    private void SetMission(string label, int reward, string key, int planetTarget, Node node)
    {
        Mission m = node as Mission;
        if(m != null) {
            m.setNewMission(label, key, reward, planetTarget);
        }
    }

    public override void _Process(double delta)
    {
        if (MissionSelection.Visible)
        {
            if (Input.IsActionPressed("mission_one"))
            {
                this.ChoiceDone(0);
            }
            else if (Input.IsActionPressed("mission_two"))
            {
                this.ChoiceDone(1);
            } else if (Input.IsActionPressed("mission_three"))
            {
                this.ChoiceDone(2);
            }
        }
    }

    private void StartMission(int i)
    {
        Mission m = this.MissionSelection.GetChildren()[i] as Mission;
        if (m != null)
        {
            _gameManager.LoadMission(m);
        }
    }

    private Mission GetMission(int i)
    {
        return MissionSelection.GetChildren()[i] as Mission;
    }

    private bool MissionIsPossible(int i) {
        if(i >= MissionSelection.GetChildren().Count) 
        {
            return false;
        }
        Mission m = GetMission(i);
        if(m != null)
        {
            return m.planetTarget == _gameManager.PlanetPosition || _gameManager.cash >= Rocket.GoingSpaceCost;
        }
        return false;
    }

    private void ChoiceDone(int choice)
	{
        if(MissionIsPossible(choice))
        {
            MissionSelection.Visible = false;
            this.StartMission(choice);
        }
	}

    private void RefreshPossibleMission()
    {
        var missions = MissionSelection.GetChildren();
        for (int i = 0; i < missions.Count; i++)
        {
            Mission m = missions[i] as Mission;
            if (m != null)
            {
               m.setDisable(MissionIsPossible(i));
            }
        }
    }

	private void SetMissionSelectVisible(bool visible_, Node2D body)
	{
        CharacterBody2D p = body as CharacterBody2D;
		if (p != null && !_gameManager.MissionInProgress())
        {
            MissionSelection.Visible = visible_;

        }
    }

    private void OnBodyEntered(Node2D body)
	{
		this.SetMissionSelectVisible(true, body);
        if(_gameManager.TimeForTheLastTravel())
        {
            SetLastMission();
        }
        RefreshPossibleMission();
	}

    private void OnBodyExited(Node2D body)
    {
        this.SetMissionSelectVisible(false, body);
    }
}
