using Godot;
using System;

public partial class PlayerJump : PlayerState
{
	protected Player Player { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

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
}
