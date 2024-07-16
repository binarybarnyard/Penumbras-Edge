using Godot;
using System;
using test_platformer.Scripts.Interfaces;

public abstract partial class State : Node, IState
{
    //Properties
    public StateMachine fsm;
    // Methods Inherited from IState
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update(double delta);
    public abstract void PhysicsUpdate(double delta);
    public abstract void HandleInput(InputEvent @event);
}
