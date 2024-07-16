using Godot;
using System;
using System.Runtime.CompilerServices;
using test_platformer.Scripts.Interfaces;

public partial class PlayerMove : PlayerState
{
    public override string Name { get; set; } = "walk";
    public override IStateMachine StateMachine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void _Ready()
	{
		Player = GetParent().GetParent<Player>();
		AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	
	public override void Enter()
	{
		GD.Print("Entering Move state.");
	}

	public override void Exit()
	{
		GD.Print("Exiting Move state.");
	}

	public override void Update(double delta)
	{
		AnimatedSprite.Play("walk");

		AnimatedSprite.FlipH = Player.Velocity.X < 0;
	}

	public override void PhysicsUpdate(double delta)
	{
		var input = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		GD.Print(Player.GetVelocityInProcess());

		if (input != 0)
		{
			Player._velocity.X = Player.Speed * input;
			Player._velocity.X = Mathf.Clamp(Player._velocity.X, -Player.Speed, Player.Speed);
		}
		else
		{
			Player._velocity.X = 0;
		}
	}

    public override void HandleInput(InputEvent @event)
    {
        throw new NotImplementedException();
    }

}
