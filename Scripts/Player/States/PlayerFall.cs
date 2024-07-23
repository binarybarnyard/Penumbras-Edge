using Godot;
using System;

public partial class PlayerFall : State
{
	// Nodes
	protected Player Player { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }

	public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void Enter()
	{
		GD.Print("Entering falling state.");
		AnimatedSprite.Play("fall");
        CheckForDeath();
	}

	public override void Exit()
	{
		GD.Print("Exiting falling state.");
		AnimatedSprite.Stop();

		if (Player.IsOnFloor())
		{
			Player._velocity.Y = 0;
		}
	}

	public override void Update(double delta)
	{
		// Flip sprite if going left
		var input = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		if (input != 0)
			AnimatedSprite.FlipH = Player._velocity.X < 0;

		// Idle if land on floor
		if (Player.IsOnFloor())
		{
			fsm.TransitionTo("PlayerIdle");
		}

		// Can attack while falling
		if (Input.IsActionJustPressed("attack"))
		{
			fsm.TransitionTo("PlayerAttack");
		}
	}
	
	public override void PhysicsUpdate(double delta)
	{
		// Left/right direction
   		var input = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		
		if (input != 0)
		{
			Player._velocity.X = Player.Speed * input;
			Player._velocity.X = Mathf.Clamp(Player._velocity.X, -Player.Speed, Player.Speed);
		}
		else
		{
			Player._velocity.X = 0;
		}

		// Gravity, Velocity, MoveandSlide
		Player.GravityForce(delta);
		Player.Velocity = Player._velocity;
		Player.MoveAndSlide();
	}

    public void CheckForDeath()
    {
        if (Player.HitPoints < 1)
        {
            fsm.TransitionTo("PlayerDead");
        }
    }
}
