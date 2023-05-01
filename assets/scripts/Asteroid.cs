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
        speed = GD.RandRange(50, 100);
        //switch (_size)
        //{
        //    case AsteroidSize.Large:
        //        
        //        _sprite.Texture = GD.Load("res://assets/sprites/space/meteors/meteorBrown_big1.png") as Texture2D;
        //        //_collisionShape.Shape = GD.Load("res://ressources/asteroid_cshape_big.tres") as Shape2D;
        //        break;
        //    case AsteroidSize.Medium:
        //        speed = GD.RandRange(100, 150);
        //        _sprite.Texture = GD.Load("res://assets/sprites/space/meteors/meteorBrown_med1.png") as Texture2D;
        //        //_collisionShape.Shape = GD.Load("res://ressources/asteroid_cshape_medium.tres") as Shape2D;
        //        break;
        //    case AsteroidSize.Small:
        //        speed = GD.RandRange(150, 200);
        //        _sprite.Texture = GD.Load("res://assets/sprites/space/meteors/meteorBrown_small1.png") as Texture2D;
        //        //_collisionShape.Shape = GD.Load("res://ressources/asteroid_cshape_small.tres") as Shape2D;
        //        break;                   
        //}
    }

    void _on_body_entered(Node node)
    {
        var instance = _missionFailledPackage.Instantiate() as Control;
        _gameManager.IsDeath = true;

        _canvasLayer.AddChild(instance);
        //_spaceShips.QueueFree();


    }
    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += movementVector.Rotated(Rotation) * speed * (float)delta;

        Vector2 cameraCenter = _camera.GlobalPosition;
        float distance = cameraCenter.DistanceTo(Position);
        if (distance > 2000 )
        {
            GD.Print("Fin de vie de l'asteroide... destruction de l'objet!");
            QueueFree();
        }
    }

    ////void _on_body_entered(Node node)
    ////{
    ////    QueueFree();
    ////}

    //void _on_player_spaceships_input_event(Node node)
    //{
    //    GD.Print("Caca");
    //}
}
