using Godot;

public partial class Spaceship : CharacterBody2D
{
    private float _acceleration = 10f;
    private float _maxSpeed = 350f;
    private double _rotationSpeed = 100f;

    public override void _PhysicsProcess(double delta)
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

    // Add death element :

 
}
