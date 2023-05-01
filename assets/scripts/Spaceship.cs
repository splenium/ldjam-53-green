using Godot;

public partial class Spaceship : CharacterBody2D
{
    [Export]
    private float _acceleration = 10f;
    [Export]
    private float _maxSpeed = 350f;
    [Export]
    private float _rotationSpeed = 125f;

    private GameManager _gameManager;

    [Export]
    private Node2D PlanetContainer;
    [Export]
    private Node2D IndicatorContainer;

    private PackedScene IndicatorPrefabs;



    public override void _Ready()
    {

        _gameManager = GetNode<GameManager>("/root/GameManager");
        IndicatorPrefabs = ResourceLoader.Load("res://assets/prefabs/indicator.tscn") as PackedScene;
        RegenIndicator();
    }

    private void RegenIndicator()
    {
        foreach(Node child in IndicatorContainer.GetChildren())
        {
            IndicatorContainer.RemoveChild(child);
        }
        foreach(Node planet in PlanetContainer.GetChildren())
        {
            SpacePlanet p = planet as SpacePlanet;
            if(p != null && _gameManager.CanLandOnPlanet(p.PlanetId))
            {
                var indicatorComponent = IndicatorPrefabs.Instantiate() as Indicator;
                indicatorComponent.rocket = this;
                indicatorComponent.planet = p;
                IndicatorContainer.AddChild(indicatorComponent);
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_gameManager.IsDeath)
        {

        }
        else
        {
            Vector2 input_vector = new(0, Input.GetAxis("forward", "backward"));
        
            Velocity += input_vector.Rotated(Rotation) * _acceleration;
            Velocity = Velocity.LimitLength(_maxSpeed);
            if (Input.IsActionPressed("right"))
            {
                Rotate((float)Mathf.DegToRad(_rotationSpeed * delta));
            }
            if (Input.IsActionPressed("left"))
            {
                Rotate((float)Mathf.DegToRad(-_rotationSpeed * delta));
            }
            if (input_vector.Y == 0)
            {
                Velocity = Velocity.MoveToward(Vector2.Zero, 3);
            }
            MoveAndSlide();
        }

    }

    // Add death element :

 
}
