using Godot;
using System;

public partial class GroundIdle : State
{
	protected GroundEnemy Enemy { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected Timer _timer { get; private set; }

	public override void _Ready()
	{
		Enemy = GetParent().GetParent<GroundEnemy>();
		AnimatedSprite = Enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_timer = Enemy.GetNode<Timer>("StateTimer");

		_timer.Connect("timeout", new Callable(this, nameof(ChangeState)));
	}

	public override void Enter()
	{
		GD.Print("Enemy entering Idle state.");
		AnimatedSprite.Play("idle");
		
		// Get a random number 
		var _idleTime = new RandomNumberGenerator();
		_idleTime.Randomize();
		float _waitTime = _idleTime.RandiRange(1, 4);

		// Set timer's wait time and start it
		_timer.WaitTime = _waitTime;
		_timer.Start();
	}

	public override void PhysicsUpdate(double delta)
	{
		// Set speed to 0
		Enemy._velocity.X = 0;

		Enemy.GravityForce(delta);
		Enemy.Velocity = Enemy._velocity;
		Enemy.MoveAndSlide();
	}

	public override void Exit()
	{
		GD.Print("Enemy exiting Idle state.");
		AnimatedSprite.Stop();
		_timer.Stop();
	}

	public void ChangeState()
	{
		fsm.TransitionTo("GroundWander");
	}
}
