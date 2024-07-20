using Godot;
using System;

public partial class Slime : GroundEnemy
{
	public StateMachine fsm;

	public override void _Ready()
	{
		fsm = GetNode<StateMachine>("StateMachine");
	}
}
