using Godot;
using System;

public partial class Asteroid : Area2D
{
    [Export]
    private AsteroidSize _size = AsteroidSize.Large;

    private CanvasLayer _canvasLayer;

    private float speed;
    private enum AsteroidSize { Large, Medium, Small }
    private Vector2 movementVector = new(0, -1);

    private Sprite2D _sprite;
    private Camera2D _camera;

    private PackedScene _missionFailledPackage;
    private CharacterBody2D _spaceShips;
    private GameManager _gameManager;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _camera = GetViewport().GetCamera2D();

        _missionFailledPackage = GD.Load("res://assets/prefabs/mission_failled_screen.tscn") as PackedScene;

        _canvasLayer = GetNode<CanvasLayer>("../../HUD");
        _spaceShips = GetNode<CharacterBody2D>("../../PlayerSpaceships");
        _gameManager = GetNode<GameManager>("/root/GameManager");

        Rotation = (float)GD.RandRange(0, 2d * MathF.PI);
        speed = GD.RandRange(50, 200);
    }

    void _on_body_entered(Node node)
    {
        var instance = _missionFailledPackage.Instantiate() as Control;
        _gameManager.IsDeath = true;

        _canvasLayer.AddChild(instance);


    }
    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += movementVector.Rotated(Rotation) * speed * (float)delta;

        Vector2 cameraCenter = _camera.GlobalPosition;
        float distance = cameraCenter.DistanceTo(Position);
        if (distance > 2000 )
        {
            QueueFree();
        }
    }
}
