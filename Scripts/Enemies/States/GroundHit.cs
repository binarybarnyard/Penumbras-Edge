using Godot;
using System;

public partial class GroundHit : State
{
	protected GroundEnemy Enemy { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

	public override void _Ready()
	{
		Enemy = GetParent().GetParent<GroundEnemy>();
		AnimatedSprite = Enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void Enter()
	{
		// Logging
		GD.Print("Enemy entering hit state.");

		// Animation
		AnimatedSprite.Play("hit");
		AnimatedSprite.SpriteFrames.SetAnimationLoop("hit", false);

		// Pause movement
		Enemy._velocity = Vector2.Zero;

		// Displace
		DisplacingForce(100);

		// Die if dead
		if (Enemy.HitPoints <= 0)
		{
			fsm.TransitionTo("GroundDead");
		}
		else
		{
			fsm.TransitionTo("GroundChase");
		}
	}

	public override void Exit()
	{
		GD.Print("Enemy exiting hit state.");
		AnimatedSprite.Stop();
	}

	public override void Update(double delta)
	{
		// Flip Sprite if left
		AnimatedSprite.FlipH = Enemy._velocity.X < 0;
	}

	public override void PhysicsUpdate(double delta)
	{
		// Gravity
		Enemy.GravityForce(delta);
		
		// Apply velocity and move
		Enemy.Velocity = Enemy._velocity;
		Enemy.MoveAndSlide();
	}

	public void DisplacingForce(int force)
	{
		var random = new RandomNumberGenerator();
		random.Randomize();

		Enemy._velocity.Y -= force;
		Enemy._velocity.X += random.RandiRange(-10, 10);
	}
}
