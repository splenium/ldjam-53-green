using Godot;
using System;

public partial class SetMenuMask : TextureRect
{
	[Export]
	public Texture MaskTexture;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var shaderMat = this.Material as ShaderMaterial;
		shaderMat.SetShaderParameter("maskTexture", MaskTexture);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
