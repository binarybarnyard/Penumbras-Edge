using Godot;
using System;

public partial class ShadowChase : State
{
	// Nodes
	protected Shadow _shadow { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected Area2D ThreatZone { get; private set; }

	// Variables
	private Vector2 _playerPosition;

	public override void _Ready()
	{
		_shadow = GetParent().GetParent<Shadow>();
		AnimatedSprite = _shadow.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		ThreatZone = _shadow.GetNode<Area2D>("ThreatZone");
	}

	public override void Enter()
	{
		GD.Print("Shadow entering Chase state.");
		AnimatedSprite.Play("walk");
	}

	public override void Exit()
	{
		GD.Print("Shadow exiting Chase state.");
		AnimatedSprite.Stop();
	}

	public override void Update(double delta)
	{
		// Flip sprite if going left
		AnimatedSprite.FlipH = _shadow._velocity.X < 0;

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
		_shadow.GravityForce(delta);

		// Generate direction towards the player position
		Vector2 direction = (_playerPosition - _shadow.GlobalPosition).Normalized();

		// Set horizontal velocity
		_shadow._velocity.X = direction.X * _shadow.Speed;

		// Move and slide with the calculated velocity
		_shadow.Velocity = _shadow._velocity;
		_shadow.MoveAndSlide();
	}
}
