using Godot;
using System;
using System.Collections;
using System.Threading.Channels;

public partial class Box : Area2D
{
    [Export]
    private Control MissionSelection { get; set; }
    private GameManager GameManager { get; set; }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        MissionSelection.Visible = false;
        GameManager = GetNode<GameManager>("/root/GameManager");

        string[] labels = new string[] {
            "Report the letter to Aliatrox",
            "Send Gift to Bibobux"
        };

        int[] cash = new int[]
        {
            10,
            12
        };

        string[] keys = new string[] {
            "E",
            "R"
        };

        for (int i = 0; i < labels.Length; i++)
        {
            this.SetMission(labels[i], cash[i], keys[i], this.MissionSelection.GetChildren()[i]);
        }
    }

    private void SetMission(string label, int cash, string key, Node node)
    {
        Mission m = node as Mission;
        if(m != null) {
            m.setNewMission(label, key, cash);
        }
    }

    public override void _Process(double delta)
    {
        if(Input.IsActionPressed("mission_one") && MissionSelection.Visible)
        {
            this.ChoiceDone(0);
        } else if (Input.IsActionPressed("mission_two") && MissionSelection.Visible)
        {
            this.ChoiceDone(1);
        }
    }

    private void StartMission(int i)
    {
        Mission m = this.MissionSelection.GetChildren()[i] as Mission;
        if (m != null)
        {
            GameManager.SetMission(m);
        }
    }

    private void ChoiceDone(int choice)
	{
        MissionSelection.Visible = false;
        this.StartMission(choice);
	}

	private void SetMissionSelectVisible(bool visible, Node2D body)
	{
        Player p = body as Player;
		if (p != null && GameManager.SelectedMission == null)
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
