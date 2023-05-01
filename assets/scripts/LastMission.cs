using Godot;
using System;

public partial class LastMission : Node2D
{
    [Export]
    private Label label;
    private GameManager _gameManager { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _gameManager = GetNode<GameManager>("/root/GameManager");
        label.Visible = false;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if(Input.IsActionJustReleased("mission_one") && label.Visible)
        {
            _gameManager.MissionSucces();
        }
	}

    private void setLabelVisible(bool visible_, Node2D body)
    {
        CharacterBody2D p = body as CharacterBody2D;
        if (p != null)
        {
            label.Visible = visible_;
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        this.setLabelVisible(true, body);
    }

    private void OnBodyExited(Node2D body)
    {
        this.setLabelVisible(false, body);
    }
}
