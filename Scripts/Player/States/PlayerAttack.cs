using Godot;
using System;

public partial class PlayerAttack : State
{
	// Timed variables
	public override bool TimedState { get; set; } = true;
	public override float MinTimeInState { get; set; } = 0.5f;
	public override float MaxTimeInState { get; set; } = 0.5f;
	public override float TimeInState { get; set; } = 0.0f;
	
	// Nodes
	protected Player Player { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected Area2D Sword { get; private set; }
	protected CollisionShape2D AttackArea { get; private set; }

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Sword = Player.GetNode<Area2D>("Sword");
		AttackArea = Sword.GetNode<CollisionShape2D>("AttackArea");

   		Sword.Connect("body_entered", new Callable(this, nameof(OnSwordBodyEntered)));
	}

	public override void Enter()
	{   
		// Logging
		GD.Print("Entering attack state");

		// Reset timer
		TimeInState = 0.0f;

		// Attack animation
		AnimatedSprite.Play("attack");

		// Open Attack hitbox
		AttackArea.Disabled = false;
	}

	public override void Exit()
	{
		GD.Print("Exiting attack state");
		AnimatedSprite.Stop();
		AttackArea.Disabled = true;
	}

	public override void Update(double delta)
	{
		// Increment State Timer
		TimeInState += (float)delta;

		// Flip hitbox if going left
		FlipHitBox();

		// Can die while attacking
		CheckForDeath();
		
		// Can transition if min time is met
		if (TimeInState >= MinTimeInState)
		{
			if (!Player.IsOnFloor())
			{
				fsm.TransitionTo("PlayerFall");
			}
			else if (Player.HitPoints > 0)
			{
				fsm.TransitionTo("PlayerIdle");
			}
		}
	}

	public override void PhysicsUpdate(double delta)
	{
		// Slide to a stop while attacking
		Player._velocity.X = (float)(Player._velocity.X / 1.15);

		// Gravity, Velocity, MoveandSlide
		Player.GravityForce(delta);
		Player.Velocity = Player._velocity;
		Player.MoveAndSlide();
	}

	public void OnSwordBodyEntered(Node body)
	{
		GD.Print(body.Name);

		// TODO: Include other enemy classes
		if (body is GroundEnemy Enemy)
		{
			Enemy.TakeDamage(Player.AttackDamage);
			GD.Print("Enemy damage taken");
		}
	}

	public void FlipHitBox()
	{
		if (AnimatedSprite.FlipH)
		{
			AttackArea.Position = new Vector2(-16f, AttackArea.Position.Y);
		}
		else
		{
			AttackArea.Position = new Vector2(16f, AttackArea.Position.Y);
		}
	}

	public void CheckForDeath()
	{
		if (Player.HitPoints < 1)
		{
			TimeInState += MinTimeInState;
			fsm.TransitionTo("PlayerDead");
		}
	}
}
