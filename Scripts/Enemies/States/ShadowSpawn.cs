using Godot;
using System;
using System.Diagnostics;

public partial class ShadowSpawn : State
{
	// Nodes
	protected Shadow _shadow { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected CircleDrawer _circleDrawer { get; private set; }
	protected Timer _timer { get; private set; }
	protected CpuParticles2D _implosion { get; private set; }
	protected GpuParticles2D _explosion { get; private set; }

	// Variables
	private Vector2 _playerPosition;

	public override void _Ready()
	{
		// Nodes
		_shadow = GetParent().GetParent<Shadow>();
		AnimatedSprite = _shadow.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_circleDrawer = _shadow.GetNode<CircleDrawer>("CircleDrawer");
		_timer = _shadow.GetNode<Timer>("SpawnTimer");
		_implosion = _shadow.GetNode<CpuParticles2D>("Implosion");
		_explosion = _shadow.GetNode<GpuParticles2D>("Explosion");

		// Signals
		_timer.Connect("timeout", new Callable(this, nameof(SpawnComplete)));
	}

	public override void Enter()
	{
		GD.Print("Shadow entering spawn state.");
		// AnimatedSprite.Play("walk");
		_timer.Start();
	}

	public override void Exit()
	{
		GD.Print("Shadow exiting spawn state.");
		AnimatedSprite.Stop();
	}

	public void SpawnComplete()
	{
		// Explode
		_explosion.Emitting = true;

		// Get rid of drawn circle
		_circleDrawer.QueueFree();

		// Stop the implosion effect
		_implosion.Emitting = false;

		// Show the sprite
		AnimatedSprite.Visible = true;

		// Start chasing
		fsm.TransitionTo("ShadowChase");
	}
}
