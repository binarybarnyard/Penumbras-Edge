using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public abstract partial class PlayerState : State, IState
{
    private Player _player;
    protected Player Player { get => _player; set => _player = value; }
    protected AnimatedSprite2D AnimatedSprite { get; set; }
    protected PlayerStateMachine _stateMachine { get; set; }
    IStateMachine IState.StateMachine
    {
        get => _stateMachine;
        set
        {
            _stateMachine = (PlayerStateMachine)value;
            StateMachine = value;
        }
    }

    public override void _Ready()
    {
        // Correct paths based on the hierarchy
        Player = GetParent().GetParent<Player>();
        _stateMachine = GetParent<PlayerStateMachine>();

        if (_stateMachine == null)
        {
            throw new InvalidCastException("PlayerState can only be used with a PlayerStateMachine.");
        }

        AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void Enter()
    {
        //GD.Print("Entering " + Name + " state.");
        AnimatedSprite.Play(AnimationName);
    }

    public override void Exit()
    {
        //GD.Print("Exiting " + Name + " state.");
        AnimatedSprite.Stop();
        _stateMachine.UpdateState();
    }
}
