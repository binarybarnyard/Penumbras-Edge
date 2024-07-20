using Godot;
using System;

public partial class GroundEnemy : RigidBody2D
{
    // Stats
	public int Damage = 1;
	public int HitPoints = 2;

	// Movement
	public Vector2 _velocity = Vector2.Zero;
	public float Speed = 300.0f;
	public float JumpVelocity = -400.0f;

	// Environment
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    // Nodes
    protected Area2D DangerZone { get; private set; }

    public override void _Ready()
    {
        DangerZone = GetNode<Area2D>("DangerZone");
        DangerZone.Connect("body_entered", new Callable(this, nameof(OnAreaEntered)));
    } 

    public virtual void TakeDamage(int _receivedDamage)
    {
        HitPoints -= _receivedDamage;
        GD.Print(HitPoints);

        if (HitPoints <= 0)
        {
            QueueFree();
        }
    }

	public virtual void OnAreaEntered(Node body)
	{
		GD.Print("Ground enemy! Ouch! Damage: " + Damage);
		if (body is Player Player)
		{
			Player.TakeDamage(Damage);
		}
	}
}
