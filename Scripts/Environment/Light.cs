using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Light : Node
{
	// Nodes
	protected Area2D LitZone { get; private set; }

	public override void _Ready()
	{
	// Nodes
	LitZone = GetNode<Area2D>("Light");

	// Signals
	LitZone.Connect("body_entered", new Callable(this, nameof(EnterLight)));
	LitZone.Connect("body_exited", new Callable(this, nameof(ExitLight)));
	}

	public void EnterLight(Node body)
	{
		if (body is Player Player)
		{
			Player.Lit = true;
		}
	}

	public void ExitLight(Node body)
	{
		if (body is Player Player)
		{
			Player.Lit = false;
		}
	}
}
