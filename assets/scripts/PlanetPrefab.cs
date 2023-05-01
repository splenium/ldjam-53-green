using Godot;
using System;

public partial class PlanetPrefab : Node2D
{
    [Export]
    public Color ColorA;
	[Export]
    public Color ColorB;

	[Export]
	public Sprite2D _sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Process(double delta)
	{
		var shaderMat = _sprite.Material as ShaderMaterial;
        shaderMat.SetShaderParameter("colorA", ColorA);
        shaderMat.SetShaderParameter("colorB", ColorB);
    }

}
