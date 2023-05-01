using Godot;
using System;

public partial class LandscapeRocket : Area2D
{
    [Export]
    private Control InfoLabel;
    [Export]
    private Label costLabel;
    [Export]
    public int GoingSpaceCost = 10;
    private GameManager _gameManager { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _gameManager = GetNode<GameManager>("/root/GameManager");
        this.InfoLabel.Visible = false;
        this.costLabel.Text = "Cost:   " + GoingSpaceCost.ToString();
    }

    private bool CanGoInSpace()
    {
        return this.GoingSpaceCost <= _gameManager.cash;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (Input.IsActionJustReleased("mission_one") && this.InfoLabel.Visible && CanGoInSpace())
        {
            this.InfoLabel.Visible = false;
            _gameManager.Pay(GoingSpaceCost);
            _gameManager.GoInSpace();
        }
    }

    private void setLabelVisible(bool visible, Node2D collided)
    {
        CharacterBody2D c = collided as CharacterBody2D;
        if (c != null)
        {
            this.InfoLabel.Visible = visible;
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
