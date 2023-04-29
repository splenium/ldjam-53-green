using Godot;
using System;

public partial class Asteroid : Area2D
{
    [Export]
    private AsteroidSize _size = AsteroidSize.Large;

    private float speed;
    private enum AsteroidSize { Large, Medium, Small }
    private Vector2 movementVector = new(0, -1);

    private Sprite2D _sprite;
    private CollisionShape2D _collisionShape;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

        Rotation = (float)GD.RandRange(0, 2d * MathF.PI);
        switch (_size)
        {
            case AsteroidSize.Large:
                speed = GD.RandRange(50, 100);
                _sprite.Texture = GD.Load("res://assets/sprites/space/meteors/meteorBrown_big1.png") as Texture2D;
                _collisionShape.Shape = GD.Load("res://ressources/asteroid_cshape_big.tres") as Shape2D;
                break;
            case AsteroidSize.Medium:
                speed = GD.RandRange(100, 150);
                _sprite.Texture = GD.Load("res://assets/sprites/space/meteors/meteorBrown_med1.png") as Texture2D;
                _collisionShape.Shape = GD.Load("res://ressources/asteroid_cshape_medium.tres") as Shape2D;
                break;
            case AsteroidSize.Small:
                speed = GD.RandRange(150, 200);
                _sprite.Texture = GD.Load("res://assets/sprites/space/meteors/meteorBrown_small1.png") as Texture2D;
                _collisionShape.Shape = GD.Load("res://ressources/asteroid_cshape_small.tres") as Shape2D;
                break;                   
        }
    }

    public void Explode()
    {
        GD.Print("Sortie de l'ecrans... destruction de l'objet!");
        QueueFree();
    }
    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += movementVector.Rotated(Rotation) * speed * (float)delta;

        var screenSize = GetViewportRect().Size;
        // var circleShape2D = (CircleShape2D)_collisionShape.Shape;
        //if (Position.Y + circleShape2D.Radius < 0 || Position.Y - circleShape2D.Radius > screenSize.Y 
        //    || Position.X + circleShape2D.Radius < 0 || Position.X - circleShape2D.Radius > screenSize.X )
        //{
        //    Explode();
        //}
    }
}
