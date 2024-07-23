using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Stats
	[Export] public int HitPoints = 5;
	[Export] public int TotalHitPoints { get; internal set; } = 5;
	[Export] public int AttackDamage = 1;
	[Export] public double Sanity = 100;
	[Export] public double MaxSanity = 100;

	// Statuses
	public bool Lit = false;

	// Movement
	public Vector2 _velocity = Vector2.Zero;
	public float Speed = 300.0f;
	public float JumpVelocity = -400.0f;

	[Signal]
	public delegate void HealthChangedEventHandler(int currentHealth, int totalHealth);
	public event HealthChangedEventHandler HealthChangedEvent;

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

	public void GravityForce(double delta)
	{
		if (!IsOnFloor())
		{
			_velocity.Y += Gravity * (float)delta;
		}
	}

	public void AdjustSanity()
	{
		if (Lit && Sanity < MaxSanity)
		{
			Sanity += 0.01;
		}
		else if (Lit)
		{
			Sanity = MaxSanity;
		}
		else if (!Lit && Sanity > 0)
		{
			Sanity -= 0.005;
		}
		else
		{
			Sanity = 0;
		}
	}

	public void TakeDamage(int damage)
	{
		if (Sanity == 0)
		{
			damage = damage * 2;
		}

		HitPoints -= damage;
		HealthChangedEvent?.Invoke(HitPoints, TotalHitPoints);
		fsm.TransitionTo("PlayerHit");
	}

	public void OnHealthChanged(int currentHealth, int totalHealth)
	{
		EmitSignal(SignalName.HealthChanged, currentHealth, totalHealth);
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
		else if (HitPoints > 0)
		{
			fsm.TransitionTo("PlayerIdle");
		}
	}
}
