
using Godot;
using System;

public partial class mission_failled_screen : Control
{
    private bool _isOnRetry = true;
    private bool _isOnQuit = false;
    private GameManager _gameManager;
    private string _menuScene = "res://scenes/game/menu.tscn";

    [Export]
    private Label LabelRetry;
    [Export]
    private Label LabelQuit;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _gameManager = GetNode<GameManager>("/root/GameManager");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (Input.IsActionJustReleased("forward") || Input.IsActionJustReleased("backward"))
        {
            _isOnRetry = !_isOnRetry;

            GD.Print(_isOnRetry);
            if (_isOnRetry)
            {
                LabelRetry.Text = "X";
                LabelQuit.Text = "";
            }
            else
            {
                LabelRetry.Text = "";
                LabelQuit.Text = "X";
            }
        }
        if (Input.IsActionJustReleased("enter"))
        {
            if (_isOnRetry)
            {
                _gameManager.AbortTheMission();
                _gameManager.IsDeath = false;
                _gameManager.LandingOn(_gameManager.PlanetPosition);
            }
            else
            {
                _gameManager.LoadScene(_menuScene);
            }
        }
    }
}
