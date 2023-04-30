using Godot;
using System;

public partial class GameManager : Node
{

	public Mission SelectedMission { get; set; }
	public int cash { get; set; }
 
	// Called when the node enters the scene tree for the first time.

	private int _startCash = 40;
	public override void _Ready()
	{
		this.newGame();
	}

	public void newGame()
	{
		this.SelectedMission = null;
		this.cash = _startCash;
	}

	public void LoadMission(Mission mission)
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
