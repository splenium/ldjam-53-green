using Godot;
using System;

public partial class GameManager : Node
{

	public Mission SelectedMission { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void SetMission(Mission mission)
	{
		this.SelectedMission = mission;
		// TODO Change scene
		GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
