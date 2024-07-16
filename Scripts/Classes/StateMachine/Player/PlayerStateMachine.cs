using Godot;
using System;

public partial class PlayerStateMachine<PlayerState> : StateMachine<T>
{
    public override void _Ready()
    {
        foreach (Node node in GetChildren())
        {
            if (node is PlayerState state)
            {
                GD.Print(node.Name);
                _states[node.Name] = state;
                state.StateMachine = this as PlayerStateMachine<PlayerState>;
            }
        }

        GD.Print("InitialState: " + InitialState);
        foreach (var property in _currentState.GetType().GetProperties())
        {
            GD.Print(property.Name + ": " + property.GetValue(_currentState));
        }

        _currentState = _states[InitialState];;
        _currentState.Enter();
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

    public void UnhandledInput(InputEvent @event)
    {
        base.UnhandledInput(@event);
        _currentState.HandleInput(@event);
    }

    public void TransitionTo(string key)
    {
        if (!_states.ContainsKey(key) || _currentState == _states[key])
        {
            return;
        }

        _currentState.Exit();
        _currentState = _states[key];
        GD.Print(DateTime.Now.ToString("HH:mm:ss.fff") + " - Transitioning to state: " + _currentState.Name);
        _currentState.Enter();
    }

    public void TransitionTo(T state)
    {
        throw new NotImplementedException();
    }

}
