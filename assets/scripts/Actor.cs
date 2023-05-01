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
    [Export]
    private bool LookLeft;
    [Export]
    private Node2D Interface;

    private Vector2 _baseScale;
    private Vector2 _interfaceBaseScale;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _gameManager = GetNode<GameManager>("/root/GameManager");
        deliveryLabel.Visible = false;
        labelName.Text = _name;

        _interfaceBaseScale = Interface.Scale;
        _baseScale = this.Scale;
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
        return _gameManager.MissionInProgress() && _gameManager.MissionPlanetTarget() == _gameManager.PlanetPosition;
    }

    public override void _Process(double delta)
    {
        if(Input.IsActionJustReleased("mission_one") && this.deliveryLabel.Visible && this.MissionIsForMe())
        {
            _gameManager.MissionSucces();
            this.deliveryLabel.Visible = false;
        }

        if (LookLeft)
        {
            Interface.Scale = new Vector2(_interfaceBaseScale.X * -1, _interfaceBaseScale.Y);
            this.Scale = new Vector2(_baseScale.X * -1, _baseScale.Y);
        } else
        {
            Interface.Scale = _interfaceBaseScale;
            this.Scale = _baseScale;
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        if(this.MissionIsForMe())
        {
            this.deliveryLabel.Text = "E  -  Give   package";
        } else
        {
            this.deliveryLabel.Text = "Nothing   to   deliver";
        }
        this.setLabelVisible(true, body);
    }

    private void OnBodyExited(Node2D body)
    {
        this.setLabelVisible(false, body);
    }
}
