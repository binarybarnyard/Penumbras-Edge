using Godot;
using System;
using System.Collections.Generic;
using test_platformer.Scripts.Interfaces;

public partial class PlayerStateMachine : StateMachine
{
    public override void _Ready()
    {
        PopulateStates();

        GD.Print("InitialState: " + InitialState);
        foreach (var property in _currentState.GetType().GetProperties())
        {
            GD.Print(property.Name + ": " + property.GetValue(_currentState));
        }

        _currentState = _states[InitialState]; ;
        _currentState.Enter();
    }

    private void PopulateStates()
    {
        foreach (Node node in GetChildren())
        {
            if (node is PlayerState state)
            {
                GD.Print(node.Name);
                _states[node.Name] = state;
                state.StateMachine = this;
            }
        }
    }


    public override void _Process(double delta)
    {
        _currentState.Update((float)delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        // GD.Print("State: " + _currentState.Name + " Physics Process Started");
        _currentState.PhysicsUpdate((float)delta);
    }
}
