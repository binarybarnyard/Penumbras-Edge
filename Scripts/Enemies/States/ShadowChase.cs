using Godot;
using System;

public partial class ShadowChase : State
{
	// Nodes
	protected Shadow _shadow { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected Area2D ThreatZone { get; private set; }
	protected Area2D DamageZone { get; private set; }
	protected Area2D LogicZone { get; private set; }
	protected RayCast2D RayCastLeft { get; private set; }
	protected RayCast2D RayCastRight { get; private set; }

	// Variables
	private Vector2 _playerPosition;
	private Vector2 _direction;

	public override void _Ready()
	{
		// Nodes
		_shadow = GetParent().GetParent<Shadow>();
		AnimatedSprite = _shadow.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		ThreatZone = _shadow.GetNode<Area2D>("ThreatZone");
   		LogicZone = _shadow.GetNode<Area2D>("LogicZone");
		DamageZone = _shadow.GetNode<Area2D>("DamageZone");
		RayCastLeft = _shadow.GetNode<RayCast2D>("RayCastLeft");
		RayCastRight = _shadow.GetNode<RayCast2D>("RayCastRight");

		// Signals
		LogicZone.Connect("body_exited", new Callable(this, nameof(TurnAround)));
		DamageZone.Connect("body_entered", new Callable(this, nameof(ApplyDamage)));

	}

	public override void Enter()
	{
		GD.Print("Shadow entering Chase state.");
		AnimatedSprite.Play("walk");

		// Sets _playerPosition
		GetPlayerPosition();

		// Sets direction to player position
		_direction = (_playerPosition - _shadow.GlobalPosition).Normalized();
		
		// Set a course to the player
		_shadow._velocity.X = _direction.X * _shadow.Speed;
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
	}

	public override void PhysicsUpdate(double delta)
	{
		// Apply Gravity
		_shadow.GravityForce(delta);

		// Check raycasts to avoid obstacles
		CheckRayCasts();

		// Move and slide with the calculated velocity
		_shadow.Velocity = _shadow._velocity;
		_shadow.MoveAndSlide();
	}

	public void TurnAround(Node body)
	{
		if (body is Player player)
		{
			// Change direction if player and shadow are moving in opposite directions
			if ((_shadow._velocity.X > 0 && player._velocity.X < 0) || (_shadow._velocity.X < 0 && player._velocity.X > 0))
			{
				_shadow._velocity.X *= -1;
			}
			else if ((_shadow._velocity.X > 0 && _shadow.Position.X > player.Position.X) || (_shadow._velocity.X < 0 && _shadow.Position.X < player.Position.X))
			{
				// Turn around if the shadow runs through the player
				_shadow._velocity.X *= -1;
			}
		}
	}

	public void ApplyDamage(Node body)
	{
		if (body is Player player)
		{
			GD.Print("Shadow enemy! Ouch! Damage: " + _shadow.Damage);
			player.TakeDamage(_shadow.Damage);
		}
	}

	public void CheckRayCasts()
	{
		// Logic for avoiding obstacles/walls
		if (RayCastLeft.IsColliding() || RayCastRight.IsColliding())
		{
			GetPlayerPosition();
			
			if (_shadow.Position.X < _playerPosition.X || _shadow.Position.X > _playerPosition.X)
			{
				_shadow._velocity.Y += _shadow.JumpVelocity;
			}
			else if (_playerPosition.Y < _shadow.Position.Y && _shadow.IsOnFloor())
			{
				_shadow._velocity.Y += _shadow.JumpVelocity;
			}
			else
			{
				_shadow._velocity.X *= -1;
			}
		}
	}
	
	public void GetPlayerPosition()
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
}
