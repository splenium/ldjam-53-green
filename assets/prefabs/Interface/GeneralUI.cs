using Godot;
using System;

public partial class GeneralUI : Control
{
    [Export]
    private Label CashLabel;
    private GameManager _gameManager { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _gameManager = GetNode<GameManager>("/root/GameManager");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        CashLabel.Text = _gameManager.cash.ToString();
	}
}
