using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public partial class PlayerJump : PlayerState
{
	public override string Name { get; set; } = "PlayerJump";
	public override IStateMachine StateMachine { get; set; }
	public override string AnimationName { get; set; } = "jump";

	public override void PhysicsUpdate(double delta)
	{
		// Update physics for Jumping aside from gravity which is handled in the parent class
		if (Player.IsOnFloor())
		{
			Exit();
		}
	}

	public override void Update(double delta)
	{
		AnimatedSprite.FlipH = _stateMachine.Velocity.X < 0;
	}

	public override void HandleInput(InputEvent @event)
	{
		if (@event.IsActionPressed("jump") && Player.IsOnFloor())
		{
			var velocity = new Vector2(_stateMachine.Velocity.X, _stateMachine.Velocity.Y);
			velocity.Y += Player.JumpVelocity;
			_stateMachine.Velocity = velocity;
		}
	}

}
