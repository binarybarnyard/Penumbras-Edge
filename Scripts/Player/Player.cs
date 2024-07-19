using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Stats
	public int HitPoints = 5;

	// Movement
	public Vector2 _velocity = Vector2.Zero;
	public float Speed = 300.0f;
	public float JumpVelocity = -400.0f;

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
		GD.Print(HitPoints);

		fsm.TransitionTo("PlayerHit");
	}

	private void OnIFrameTimeout()
	{
		// Hitbox remains disabled if dead
		if (HitPoints > 0)
		{
			GD.Print("Hitbox enabled.");
			CollisionLayer = 1;
		}

		// State transition after damage
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
