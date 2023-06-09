using Godot;
using System;

public partial class Mission : MarginContainer
{
	public string MissionLabel;
	private string MissionKey;
	public int reward { get; set; }
	public int planetTarget { get; set; }

	[Export]
	private Label labelMission;
    [Export]
    private Label labelCash;
    [Export]
	private Label key;
	[Export]
	private ColorRect DisableComponent;

    public void setNewMission(string missionLabel, string missionKey, int reward, int planetTarget)
	{
		this.MissionLabel = missionLabel;
		this.MissionKey = missionKey;
		this.reward = reward;
		this.planetTarget = planetTarget;

        this.labelMission.Text = missionLabel;
		this.key.Text = missionKey;
		this.labelCash.Text = "reward: " + reward.ToString();
	}

	public void setDisable(bool disable)
	{
		DisableComponent.Visible = !disable;

    }
}
