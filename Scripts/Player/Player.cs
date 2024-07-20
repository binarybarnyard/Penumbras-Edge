using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Stats
	[Export]
	public int HitPoints = 5;
	[Export]
	public int TotalHitPoints { get; internal set; } = 15;

	// Movement
	public Vector2 _velocity = Vector2.Zero;
	public float Speed = 300.0f;
	public float JumpVelocity = -400.0f;

	[Signal]
	public delegate void HealthChangedEventHandler(int health);

	// Environment
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Objects
	public StateMachine fsm;
	public Timer IFrameTimer { get; private set; }

	public override void _Ready()
	{
		fsm = GetNode<StateMachine>("StateMachine");
		IFrameTimer = GetNode<Timer>("iFrame");
	}

	public void TakeDamage(int damage)
	{
		HitPoints -= damage;
		OnHealthChanged(HitPoints);
		fsm.TransitionTo("PlayerHit");
	}

	public void OnHealthChanged(int health)
	{
		GD.Print("Emitting HealthChanged with health: " + health);
		EmitSignal(nameof(HealthChangedEventHandler), health);
	}

	private void OnIFrameTimeout()
	{
		if (HitPoints > 0)
		{
			GD.Print("Hitbox enabled.");
			CollisionLayer = 1;
		}

		if (!IsOnFloor())
		{
			fsm.TransitionTo("PlayerFall");
		}
		else
		{
			fsm.TransitionTo("PlayerIdle");
		}
	}
}
