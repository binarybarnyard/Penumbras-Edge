using Godot;
using System;

public partial class GroundChase : State
{
	// Nodes
	protected GroundEnemy Enemy { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected Area2D ThreatZone { get; private set; }

	// Variables
	private Vector2 _playerPosition;

	public override void _Ready()
	{
		Enemy = GetParent().GetParent<GroundEnemy>();
		AnimatedSprite = Enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		ThreatZone = Enemy.GetNode<Area2D>("ThreatZone");
	}

	public override void Enter()
	{
		GD.Print("Enemy entering Chase state.");
		AnimatedSprite.Play("chase");
	}

	public override void Exit()
	{
		GD.Print("Enemy exiting Chase state.");
		AnimatedSprite.Stop();
	}

	public override void Update(double delta)
	{
		// Check if the player is within the threat zone
		if (ThreatZone.GetOverlappingBodies().Count > 0)
		{
			// Loop through all overlapping bodies
			foreach (var body in ThreatZone.GetOverlappingBodies())
			{
				if (body is Player player)
				{
					_playerPosition = player.GlobalPosition;
					return; // Exit after finding the player
				}
			}
		}
	}

	public override void PhysicsUpdate(double delta)
	{
		// Apply Gravity
		Enemy.GravityForce(delta);

		// Generate direction towards the player position
		Vector2 direction = (_playerPosition - Enemy.GlobalPosition).Normalized();

		// Ensure only horizontal movement
		direction.Y = 0;

		// Set horizontal velocity
		Enemy._velocity.X = direction.X * Enemy.Speed;

		// Move and slide with the calculated velocity
		Enemy.Velocity = Enemy._velocity;
		Enemy.MoveAndSlide();
	}
}
