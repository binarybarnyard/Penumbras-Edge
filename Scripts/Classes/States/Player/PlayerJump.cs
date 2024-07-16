using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public partial class PlayerJump : PlayerState
{
	public override string Name { get; set; } = "jump";
	public override IStateMachine StateMachine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void Enter()
	{
		GD.Print("Entering Jump state.");
	}

	public override void Exit()
	{
		GD.Print("Exiting Jump state.");
	}

	public override void PhysicsUpdate(double delta)
	{
		Player._velocity = Player.GetVelocityInProcess();
		GD.Print(Player._velocity);

		Player._velocity.Y += Player.JumpVelocity;
	}

	public override void Update(double delta)
	{
		throw new NotImplementedException();
	}

	public override void HandleInput(InputEvent @event)
	{
		throw new NotImplementedException();
	}

}
