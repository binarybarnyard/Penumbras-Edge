using Godot;
using System;

public partial class GroundDead : State
{
	protected GroundEnemy Enemy { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected Area2D DamageZone { get; private set; }
	protected Area2D ThreatZone { get; private set; }


	public override void _Ready()
	{
		Enemy = GetParent().GetParent<GroundEnemy>();
		AnimatedSprite = Enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		DamageZone = Enemy.GetNode<Area2D>("DamageZone");
		ThreatZone = Enemy.GetNode<Area2D>("ThreatZone");
	}

	public override void Enter()
	{
		// Logging
		GD.Print("Enemy entering dead state.");

		// Stop movement
		Enemy._velocity = Vector2.Zero;
		
		// Animation
		AnimatedSprite.Play("dead");
		AnimatedSprite.SpriteFrames.SetAnimationLoop("dead", false);

		// Disable hitbox and areas
		DamageZone.GetNode<CollisionShape2D>("Damage").CallDeferred("set_disabled", true);
		ThreatZone.GetNode<CollisionShape2D>("Threat").CallDeferred("set_disabled", true);
		Enemy.CollisionLayer = 0;
		Enemy.CollisionMask = 0;
	}

	public override void Exit()
	{
		GD.Print("Enemy exiting dead state.");
		AnimatedSprite.Stop();
	}

	public override void Update(double delta)
	{
		// Flip Sprite if left
		AnimatedSprite.FlipH = Enemy._velocity.X < 0;
	}

	public override void PhysicsUpdate(double delta)
	{
		// // Gravity
		// Enemy.GravityForce(delta);
		
		// Apply velocity and move
		Enemy.Velocity = Enemy._velocity;
		Enemy.MoveAndSlide();
	}
}
