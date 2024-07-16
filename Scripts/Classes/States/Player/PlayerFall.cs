using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public partial class PlayerFall : PlayerState
{
	public override string Name { get; set; } = "PlayerFall";
	public override IStateMachine StateMachine { get; set; }
	public override string AnimationName { get; set; } = "fall";

	public override void HandleInput(InputEvent @event)
	{
		// What should happen when the player is falling and input is received

	}

	public override void PhysicsUpdate(double delta)
	{
		if (Player.IsOnFloor())
		{
			Exit();
		}

	}

	public override void Update(double delta)
	{
		AnimatedSprite.Play("fall");
		AnimatedSprite.FlipH = Player.Velocity.X < 0;
	}
}
