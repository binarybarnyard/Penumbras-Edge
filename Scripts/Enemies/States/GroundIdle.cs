using Godot;
using System;

public partial class GroundIdle : State
{
	protected Slime Slime { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

	public override void _Ready()
	{
		Slime = GetParent().GetParent<Slime>();
		AnimatedSprite = Slime.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void Enter()
	{
		GD.Print("Entering Idle state.");
		AnimatedSprite.Play("idle");

		Slime._velocity = Vector2.Zero;
	}

	public override void Exit()
	{
		GD.Print("Exiting Idle state.");
		AnimatedSprite.Stop();
	}
}
