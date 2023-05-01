using Godot;
using System;

public partial class HandleMenu : Control
{
    private GameManager _gameManager { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>("/root/GameManager");
        DisplayCredit(_gameManager.MustShowCreditsFirst);
    }

    private bool _isOnDeliver = true;
    private bool _isShowingCredits = false;

    [Export]
    private Label LabelDeliver;
    [Export]
    private Label LabelCredits;

    [Export]
    private TextureRect CreditsTexture;

    [Export]
    private ColorRect Fader;

    private bool _isLeaving = false;

    private async void FadeOut()
    {
        float duration = 2.0f;
        float stepspersec = 30.0f;

        float steptime = 1.0f / stepspersec;

        float time = 0.0f;
        Fader.Color = new Color(0, 0, 0, 0);
        while (time < duration)
        {
            await ToSignal(GetTree().CreateTimer(steptime), "timeout");
            Fader.Color = new Color(0, 0, 0, time / duration);
            time += steptime;
        }
        Fader.Color = new Color(0, 0, 0, 1);
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        StartGame();
    }

    private void StartGame()
    {
        _gameManager.NewGame();
        _gameManager.LandingOn(_gameManager.PlanetPosition);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (_isLeaving)
            return;
        if (_isShowingCredits)
        {
            if (Input.IsActionJustReleased("echap"))
            {
                DisplayCredit(false);
            }
        }
        else
        {
            if (Input.IsActionJustReleased("forward") || Input.IsActionJustReleased("backward"))
                _isOnDeliver = !_isOnDeliver;

            if (_isOnDeliver)
            {
                LabelDeliver.Text = "X";
                LabelCredits.Text = "";
            }
            else
            {
                LabelDeliver.Text = "";
                LabelCredits.Text = "X";
            }

            if (Input.IsActionJustReleased("enter"))
            {
                if (_isOnDeliver)
                {
                    _isLeaving = true;
                    FadeOut();
                }
                else
                {
                    DisplayCredit(true);
                }
            }

        }
    }

    private void DisplayCredit(bool display)
    {
        CreditsTexture.Visible = display;
        _isShowingCredits = display;
    }
}
