using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public partial class PlayerIdle : PlayerState
{
	public override string Name { get; set; } = "idle";
	public override IStateMachine StateMachine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public override void Enter()
	{
		GD.Print("Entering Idle state.");
		AnimatedSprite.Play("idle");
	}

	public override void Exit()
	{
		GD.Print("Exiting Idle state.");
		AnimatedSprite.Stop();
	}

	public override void HandleInput(InputEvent @event)
	{
		throw new NotImplementedException();
	}

	public override void PhysicsUpdate(double delta)
	{
		throw new NotImplementedException();
	}

	public override void Update(double delta)
	{
		throw new NotImplementedException();
	}

}
