using Godot;
using System;

public partial class Slime : GroundEnemy
{
	protected GroundEnemy Enemy { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

	public override void _Ready()
	{
		base._Ready();
		fsm = GetNode<StateMachine>("StateMachine");
	}
}
