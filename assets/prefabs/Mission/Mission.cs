using Godot;
using System;

public partial class Mission : MarginContainer
{
	private string MissionLabel;
	private string MissionKey;
	private int cash;

	[Export]
	private Label labelMission;
    [Export]
    private Label labelCash;
    [Export]
	private Label key;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void setNewMission(string missionLabel, string missionKey, int cash)
	{
		GD.Print(missionLabel);
		this.MissionLabel = missionLabel;
		this.MissionKey = missionKey;
		this.cash = cash;

		this.labelMission.Text = missionLabel;
		this.key.Text = missionKey;
		this.labelCash.Text = cash.ToString() + "$";
	}
}
