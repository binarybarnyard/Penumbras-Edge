using Godot;
using System;

public partial class GroundWander : State
{
	protected GroundEnemy Enemy { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected Area2D WanderZone { get; private set; }
	protected CollisionShape2D CollisionShape2D { get; private set; }

	private RandomNumberGenerator rng = new RandomNumberGenerator();
	private Vector2 _targetPosition;

	public override void _Ready()
	{
		Enemy = GetParent().GetParent<GroundEnemy>();
		AnimatedSprite = Enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		WanderZone = Enemy.GetParent().GetNode<Area2D>("WanderZone");
		CollisionShape2D = WanderZone.GetNode<CollisionShape2D>("Wander");
	}

	public override void Enter()
	{
		GD.Print("Enemy entering wander state.");
		AnimatedSprite.Play("idle");

		SetRandomTargetPosition();
	}

	public override void Exit()
	{
		GD.Print("Enemy exiting wander state.");
		AnimatedSprite.Stop();
	}

	public override void Update(double delta)
	{
		base.Update(delta);

		AnimatedSprite.FlipH = Enemy._velocity.X < 0;

		// Check if reached target position
		if (Enemy.GlobalPosition.DistanceTo(_targetPosition) < 10f)
		{
			fsm.TransitionTo("GroundIdle");
		}
	}

	public override void PhysicsUpdate(double delta)
	{
		base.PhysicsUpdate(delta);
		Enemy.GravityForce(delta);

		// Generate direction towards target position
		Vector2 direction = (_targetPosition - Enemy.GlobalPosition).Normalized();
		
		// Velocity + MovAndSlide
		Enemy._velocity = direction * Enemy.Speed;
		Enemy.Velocity = Enemy._velocity;
		Enemy.MoveAndSlide();
	}

	private void SetRandomTargetPosition()
	{
		if (CollisionShape2D.Shape is RectangleShape2D rectangle)
		{
			Vector2 size = rectangle.Size;
			float minX = -size.X / 2;
			float maxX = size.X / 2;
			float minY = -size.Y / 2;
			float maxY = size.Y / 2;

			// Generate random x and y within the bounds
			float randomX = rng.RandfRange(minX, maxX);
			float randomY = rng.RandfRange(minY, maxY);

			// Adjust for the position of the Area2D
			_targetPosition = WanderZone.GlobalPosition + new Vector2(randomX, randomY);
		}
		else
		{
			GD.PrintErr("Unsupported shape type for random point generation.");
			_targetPosition = Enemy.GlobalPosition; // Default to current position
		}
	}

}
