using Godot;
using System;

public partial class Actor : Area2D
{
    [Export]
    private string _name;
    [Export]
    private Label deliveryLabel;
    [Export]
    private Label labelName;
    private GameManager _gameManager { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _gameManager = GetNode<GameManager>("/root/GameManager");
        deliveryLabel.Visible = false;
        labelName.Text = _name;
	}

    private void setLabelVisible(bool visible, Node2D collided)
    {
        CharacterBody2D c = collided as CharacterBody2D;
        if (c != null)
        {
            this.deliveryLabel.Visible = visible;
        }
    }

    private bool MissionIsForMe()
    {
        return _gameManager.SelectedMission != null && _gameManager.SelectedMission.planetTarget == _gameManager.PlanetPosition;
    }

    public override void _Process(double delta)
    {
        if(Input.IsActionJustReleased("mission_one") && this.deliveryLabel.Visible && this.MissionIsForMe())
        {
            _gameManager.MissionSucces();
            this.deliveryLabel.Visible = false;
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        if(this.MissionIsForMe())
        {
            this.deliveryLabel.Text = "E to give package";
        } else
        {
            this.deliveryLabel.Text = "Nothing to deliver";
        }
        this.setLabelVisible(true, body);
    }

    private void OnBodyExited(Node2D body)
    {
        this.setLabelVisible(false, body);
    }
}
