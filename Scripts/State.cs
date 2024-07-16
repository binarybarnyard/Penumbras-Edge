using Godot;
using System;

public partial class State : Node
{
    public StateMachine fsm;

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public override void _Ready()
    {
    }

    public virtual void Update(double delta)
    {
    }

    public virtual void PhysicsUpdate(double delta)
    {
    }

    public virtual void HandleInput(InputEvent @event)
    {
    }
}
