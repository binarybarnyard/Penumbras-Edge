using Godot;
using System;

public partial class PlayerHit : State
{
	protected Player Player { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void Enter()
	{
		// Logging
		GD.Print("Entering hit state");

		// Animation
		AnimatedSprite.Play("hit");

		// Disable Hitbox
		Player.CollisionLayer = 0;

		// Start iframe timer
		Player.IFrameTimer.Start();

		// Displace
		DisplacingForce(100);
	}

	public override void Exit()
	{
		GD.Print("Exiting hit state");
		AnimatedSprite.Stop();
		Player.GetNode<Timer>("iFrame").Start();
	}

	public override void Update(double delta)
	{
		// Flip Sprite if left
		AnimatedSprite.FlipH = Player._velocity.X < 0;

		if (Player.HitPoints <= 0)
		{
			fsm.TransitionTo("PlayerDead");
		}

	}

	public override void PhysicsUpdate(double delta)
	{
		// Gravity
		GravityForce(delta);

		// Movement
		HandleMovement();
		
		// Apply velocity and move
		Player.Velocity = Player._velocity;
		Player.MoveAndSlide();
	}

	public void GravityForce(double delta)
	{
		if (!Player.IsOnFloor())
		{
			Player._velocity.Y += Player.Gravity * (float)delta;
		}
	}

	public void HandleMovement()
	{
		// Left/right direction
		var input = Input.GetActionStrength("right") - Input.GetActionStrength("left");

		if (input != 0)
		{
			Player._velocity.X = Player.Speed * input;
			Player._velocity.X = Mathf.Clamp(Player._velocity.X, -Player.Speed, Player.Speed);
		}
	}

	public void DisplacingForce(int force)
	{
		var random = new RandomNumberGenerator();
		random.Randomize();

		Player._velocity.Y -= force;
		Player._velocity.X += random.RandiRange(-10, 10);
	}
}
