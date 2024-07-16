using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public string InitialStateName = "PlayerIdle";
	[Export]
	public float Speed = 300.0f;
	[Export]
	public const float JumpVelocity = -400.0f;

	public PlayerStateMachine stateMachine;

	public override void _Ready()
	{
		stateMachine = new PlayerStateMachine(InitialStateName);
		GD.Print("Player Ready");
	}

	public override void _Process(double delta)
	{
		if (stateMachine != null)
		{
			stateMachine._Process(delta);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (stateMachine != null)
		{
			stateMachine._PhysicsProcess(delta);
		}
		MoveAndSlide();
	}








	// // Animation
	// private AnimatedSprite2D _animatedSprite;

	// private StateMachine _stateMachine;
	// private IdleState _idleState;

	// public override void _Ready()
	// {
	// 	base._Ready();

	// 	_stateMachine = new StateMachine();
	// 	_idleState = new IdleState();

	// 	_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	// }

	// public override void _PhysicsProcess(double delta)
	// {
	// 	HandleMovement();
	// 	HandleJump();
	// 	UpdateAnimation(); 

	// 	// Velocity = _velocity;
	// 	// MoveAndSlide();
	// }

	// private void HandleMovement()
	// {
	// 	Vector2 direction = Input.GetVector("left", "right", "up", "down");

	// 	if (direction != Vector2.Zero)
	// 	{
	// 		_velocity.X = direction.X * Speed;
	// 	}
	// 	else
	// 	{
	// 		_velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
	// 	}
	// }

	// private void HandleJump()
	// {
	// 	if (Input.IsActionJustPressed("jump") && IsOnFloor())
	// 	{
	// 		_velocity.Y = JumpVelocity;
	// 	}
	// }

	// private void UpdateAnimation()
	// {
	// 	// Update the animation based on the player's state
	// 	if (IsOnFloor())
	// 	{
	// 		if (_velocity.X == 0)
	// 		{
	// 			_animatedSprite.Play("idle");
	// 		}
	// 		else
	// 		{
	// 			_animatedSprite.Play("walk");
	// 		}
	// 	}
	// 	else
	// 	{
	// 		_animatedSprite.Play("jump");
	// 	}

	// 	// Horizontal flip based on direction moved
	// 	if (_velocity.X > 0)
	// 	{
	// 		_animatedSprite.FlipH = false;
	// 	}
	// 	else if (_velocity.X < 0)
	// 	{
	// 		_animatedSprite.FlipH = true;
	// 	}
	// }

}
