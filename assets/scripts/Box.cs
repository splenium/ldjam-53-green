using Godot;
using System;
using System.Collections;
using System.Threading.Channels;

public partial class Box : Area2D
{
    [Export]
    private Control MissionSelection { get; set; }
    private GameManager _gameManager { get; set; }

    [Export]
    private string[] labels;
    [Export]
    private int[] rewards;
    [Export]
    private string[] keys;
    [Export]
    private int[] planetTarget;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        MissionSelection.Visible = false;
        _gameManager = GetNode<GameManager>("/root/GameManager");
        _gameManager.SelectedMission = null;

        for (int i = 0; i < labels.Length; i++)
        {
            this.SetMission(labels[i], rewards[i], keys[i], planetTarget[i], this.MissionSelection.GetChildren()[i]);
        }
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

    private void ChoiceDone(int choice)
	{
        MissionSelection.Visible = false;
        this.StartMission(choice);
	}

	private void SetMissionSelectVisible(bool visible, Node2D body)
	{
        CharacterBody2D p = body as CharacterBody2D;
		if (p != null && _gameManager.SelectedMission == null)
        {
            MissionSelection.Visible = visible;
        }
    }

    private void OnBodyEntered(Node2D body)
	{
		this.SetMissionSelectVisible(true, body);
	}

    private void OnBodyExited(Node2D body)
    {
        this.SetMissionSelectVisible(false, body);
    }
}
