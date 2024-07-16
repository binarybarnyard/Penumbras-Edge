using Godot;
using System;

public partial class PlayerFall : State
{
	// Nodes
	protected Player Player { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void Enter()
	{
		GD.Print("Entering falling state.");
	}

	public override void Exit()
	{
		GD.Print("Exiting falling state.");

		if (Player.IsOnFloor())
		{
			Player._velocity.Y = 0;
			GD.Print(Player.GetVelocityInProcess());
		}
	}

	public override void HandleInput(InputEvent @event)
	{
	}

	public override void PhysicsUpdate(double delta)
	{
		Player.GravityForce(delta);
	}
}
