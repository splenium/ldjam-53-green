using Godot;
using System;

public partial class PlanetPrefab : Node2D
{
    [Export]
    private Color ColorA;
	[Export]
    private Color ColorB;

	[Export]
	private Sprite2D _sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var shaderMat = _sprite.Material as ShaderMaterial;
        shaderMat.SetShaderParameter("colorA", ColorA);
        shaderMat.SetShaderParameter("colorB", ColorB);
    }

}
