using Godot;
using System;

public partial class TreeVariation : Node2D
{
	[Export]
	private TextureRect TexRect;
    [Export]
    private float BranchAngleFactor = 0.6f;
    [Export]
    private Color BarkColor = new Color(0, 0, 0, 1);
    [Export]
    private Color LeafColor = new Color(1,0,0,1);


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        var shaderMat = TexRect.Material as ShaderMaterial;
        shaderMat.SetShaderParameter("_barkColor", BarkColor);
        shaderMat.SetShaderParameter("_leafColor", LeafColor);
        shaderMat.SetShaderParameter("_branchAngleFactor", BranchAngleFactor);
        shaderMat.SetShaderParameter("_seed", this.Position.X);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
