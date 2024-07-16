using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public partial class PlayerIdle : PlayerState
{

	public override string Name { get; set; } = "PlayerIdle";
	public override IStateMachine StateMachine { get; set; }
	public override string AnimationName { get; set; } = "idle";

	public override void HandleInput(InputEvent @event)
	{
	}

	public override void PhysicsUpdate(double delta)
	{
		if (_stateMachine.Velocity != Vector2.Zero)
		{
			Exit();
		}
	}

	public override void Update(double delta)
	{
		//No-op
	}

}
