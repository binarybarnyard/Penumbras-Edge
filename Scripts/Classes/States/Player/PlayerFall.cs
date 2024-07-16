using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public partial class PlayerFall : PlayerState
{
	public override string Name { get; set; } = "fall";
	public override IStateMachine StateMachine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


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

	public override void Update(double delta)
	{
		throw new NotImplementedException();
	}
}
