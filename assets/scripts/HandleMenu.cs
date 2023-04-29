using Godot;
using System;

public partial class HandleMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        CreditsTexture.Visible = false;
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

    private async void FadeOut()
    {
        float duration = 2.0f;
        float stepspersec = 30.0f;

        float steptime = 1.0f/ stepspersec;

        float time = 0.0f;
        Fader.Color = new Color(0, 0, 0, 0);
        while (time < duration)
        {
            await ToSignal(GetTree().CreateTimer(steptime), "timeout");
            Fader.Color = new Color(0, 0, 0, time / duration);
            time += steptime;
        }
        Fader.Color = new Color(0, 0, 0,1);
        await ToSignal(GetTree().CreateTimer(1), "timeout");
        GetTree().ChangeSceneToFile("res://scenes/game/PlanetScene.tscn");

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		if (_isShowingCredits)
		{
            if (Input.IsActionJustReleased("echap"))
            {
                _isShowingCredits = false;
                CreditsTexture.Visible = false;
            }
		}
		else
		{
            if (Input.IsActionJustReleased("forward") || Input.IsActionJustReleased("backward"))
                _isOnDeliver = !_isOnDeliver;

            if (_isOnDeliver)
            {
                LabelDeliver.Text = ">";
                LabelCredits.Text = "";
            }
            else
            {
                LabelDeliver.Text = "";
                LabelCredits.Text = ">";
            }

            if (Input.IsActionJustReleased("ente" +
                "" +
                "r"))
            {
                if (_isOnDeliver)
                    FadeOut();// TODO start game
                else
                {
                    CreditsTexture.Visible = true;
                    _isShowingCredits = true;
                }
            }

        }
    }
}
