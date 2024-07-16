using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public abstract partial class PlayerState : State
{
    protected Player Player { get; private set; }
    protected AnimatedSprite2D AnimatedSprite { get; private set; }
    public PlayerStateMachine<PlayerState> StateMachine { get; set; }

    public override void _Ready()
    {
        Player = GetParent().GetParent<Player>();
        AnimatedSprite = Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        StateMachine = GetParent<PlayerStateMachine<PlayerState>>();
    }
}
