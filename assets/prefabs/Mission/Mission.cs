using Godot;
using System;

public partial class Mission : MarginContainer
{
	public string MissionLabel;
	private string MissionKey;
	public int cash { get; set; }

	[Export]
	private Label labelMission;
    [Export]
    private Label labelCash;
    [Export]
	private Label key;

	public void setNewMission(string missionLabel, string missionKey, int cash)
	{
		GD.Print(missionLabel);
		this.MissionLabel = missionLabel;
		this.MissionKey = missionKey;
		this.cash = cash;

		this.labelMission.Text = missionLabel;
		this.key.Text = missionKey;
		this.labelCash.Text = cash.ToString();
	}
}
