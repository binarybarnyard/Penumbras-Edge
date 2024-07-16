using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public abstract partial class State : Node, IState
{

    //Properties
    public new abstract string Name { get; set; }
    public abstract IStateMachine StateMachine { get; set; }

    // Methods Inherited from IState

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update(double delta);
    public abstract void PhysicsUpdate(double delta);
    public abstract void HandleInput(InputEvent @event);
}
