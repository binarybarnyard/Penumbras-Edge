using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public abstract partial class PlayerState : State, IState
{
    protected Player Player { get; set; }
    protected AnimatedSprite2D AnimatedSprite { get; set; }
    private PlayerStateMachine _stateMachine { get; set; }
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
        Player = GetParent().GetParent<Player>();
        AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _stateMachine = GetParent<PlayerStateMachine>();
    }
}
