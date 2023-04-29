using Godot;
using System;
using System.Threading.Channels;

public partial class Box : Area2D
{
    [Export]
    private Label label { get; set; }

	private bool alreadyOpen = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		label.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(label.Visible)
		{
			if(Input.IsActionJustReleased("take_delivery"))
			{
				GD.Print("GOOD");
				this.ChoiceDone();
			} else if(Input.IsActionJustReleased("steal_delivery"))
			{
				GD.Print("BAD");
				this.ChoiceDone();

            }
		}
	}

	private void ChoiceDone()
	{
		this.alreadyOpen = true;
		label.Visible = false;
	}

	private void ChangeLabelVisible(bool visible, Node2D body)
	{
        Player p = body as Player;
		if (p != null && !this.alreadyOpen)
        {
            label.Visible = visible;
        }
    }

    private void OnBodyEntered(Node2D body)
	{
		this.ChangeLabelVisible(true, body);
	}

    private void OnBodyExited(Node2D body)
    {
        this.ChangeLabelVisible(false, body);
    }
}
