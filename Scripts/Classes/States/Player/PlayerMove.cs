using Godot;
using System;
using System.Runtime.CompilerServices;
using test_platformer.Scripts.Interfaces;

public partial class PlayerMove : PlayerState
{
	public override string Name { get; set; } = "PlayerWalk";
	public override IStateMachine StateMachine { get; set; }
	public override string AnimationName { get; set; } = "walk";

	public override void Update(double delta)
	{
		AnimatedSprite.FlipH = _stateMachine.Velocity.X < 0;
	}

	public override void PhysicsUpdate(double delta)
	{
		var input = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		//GD.Print(" PlayerMove_PhysicsUpdate Velocity: " + _stateMachine.Velocity);
		var velocity = new Vector2(_stateMachine.Velocity.X, _stateMachine.Velocity.Y);

		if (input != 0 && Player.IsOnFloor())
		{
			velocity.X = Player.Speed * input;
			velocity.X = Mathf.Clamp(velocity.X, -Player.Speed, Player.Speed);
		}
		else
		{
			Exit();
		}
	}

	public override void HandleInput(InputEvent @event)
	{
		throw new NotImplementedException();
	}

}
