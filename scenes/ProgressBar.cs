using Godot;
using System;

public partial class ProgressBar : TextureProgressBar
{
    [Export]
    private Timer _delivryTimer;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Value = _delivryTimer.TimeLeft;
		GD.Print(Value);
	}
}
