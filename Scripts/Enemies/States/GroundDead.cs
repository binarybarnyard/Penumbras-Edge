using Godot;
using System;

public partial class GroundDead : State
{
	[Export] public PackedScene DropScene {get; set; }
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
		
		// Drop loot on death
		LootOnDeath();
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

		// Corpse is left behind, collision disabled
		if (Enemy.IsOnFloor())
		{
			Enemy.Gravity = 0;
			Enemy.ZIndex = -1;
			Enemy.CollisionLayer = 0;
			Enemy.CollisionMask = 0;
		}
	}

	public override void PhysicsUpdate(double delta)
	{	
		

		// Apply velocity and move
		Enemy.Velocity = Enemy._velocity;
		Enemy.MoveAndSlide();
	}

	public void LootOnDeath()
	{
		// Check if the DropScene is set
		if (DropScene != null)
		{
			// Instance the scene
			Node2D dropInstance = (Node2D)DropScene.Instantiate();

			// Set the position of the drop instance slightly above the current position
			dropInstance.Position = Enemy.GlobalPosition + new Vector2(0, -15); // Adjust the Y value as needed

			// Use call_deferred to add the instance to the scene tree
			CallDeferred(nameof(AddChildToParent), dropInstance);
		}
	}

	private void AddChildToParent(Node2D dropInstance)
	{
		GetParent().GetParent().AddChild(dropInstance);
	}
}
