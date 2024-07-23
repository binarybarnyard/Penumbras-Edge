using Godot;
using System;

public partial class PlayerDead : State
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
		GD.Print("Enter dead state");
		
		// Stopping timer disables hitbox
		Player.IFrameTimer.Stop();

		// No horizontal movement
		Player._velocity.X = 0;

		// Animation
  		AnimatedSprite.Play("dead");
		AnimatedSprite.SpriteFrames.SetAnimationLoop("dead", false);

	}

	public override void Exit()
	{
		GD.Print("Exit dead state");
	}

	public override void Update(double delta)
	{
		if (Player.IsOnFloor())
		{
			Player.CollisionMask = 0;
			Player.CollisionLayer = 0;
		}
	}

	public override void PhysicsUpdate(double delta)
	{		
		// Gravity if not on floor (see update fn)
		if (Player.CollisionLayer != 0)
		{
			Player.GravityForce(delta);
		}
		
		// Apply velocity and move
		Player.Velocity = Player._velocity;
		Player.MoveAndSlide();
	}
}
