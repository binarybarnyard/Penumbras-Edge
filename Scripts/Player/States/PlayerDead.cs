using Godot;
using System;

public partial class PlayerDead : State
{
	protected Player Player { get; private set; }
	protected AnimationPlayer AnimationPlayer { get; private set; }

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimationPlayer = Player.GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void Enter()
	{
		// Logging
		GD.Print("Enter dead state");
		
		// Stopping timer disables hitbox
		Player.IFrameTimer.Stop();

		// No horizontal movement
		Player._velocity.X = 0;

		// Animation
		AnimationPlayer.Play("dead");
	}

	public override void Exit()
	{
		GD.Print("Exit dead state");
	}

	public override void PhysicsUpdate(double delta)
	{		
		// Gravity, Velocity, MoveAndSlide		
		Player.GravityForce(delta);
		Player.Velocity = Player._velocity;
		Player.MoveAndSlide();
	}
}
