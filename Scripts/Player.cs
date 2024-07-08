using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Movement
	public Vector2 _velocity = Vector2.Zero;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Environment
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Animation
	private AnimatedSprite2D _animatedSprite;


	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleGravity(delta);
		HandleMovement();
		HandleJump();
		UpdateAnimation(); 

		Velocity = _velocity;
		MoveAndSlide();
	}

	public void HandleGravity(double delta)
	{
		if (!IsOnFloor())
		{
			_velocity.Y += gravity * (float)delta;
		}
	}

	private void HandleMovement()
	{
   		Vector2 direction = Input.GetVector("left", "right", "up", "down");

		if (direction != Vector2.Zero)
		{
			_velocity.X = direction.X * Speed;
		}
		else
		{
			_velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
	}

	private void HandleJump()
	{
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			_velocity.Y = JumpVelocity;
		}
	}

	private void UpdateAnimation()
	{
		// Update the animation based on the player's state
		if (IsOnFloor())
		{
			if (_velocity.X == 0)
			{
				_animatedSprite.Play("idle");
			}
			else
			{
				_animatedSprite.Play("walk");
			}
		}
		else
		{
			_animatedSprite.Play("jump");
		}

		// Horizontal flip based on direction moved
		if (_velocity.X > 0)
		{
			_animatedSprite.FlipH = false;
		}
		else if (_velocity.X < 0)
		{
			_animatedSprite.FlipH = true;
		}
	}
}
