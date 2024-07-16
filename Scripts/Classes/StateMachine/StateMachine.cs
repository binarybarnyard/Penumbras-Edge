using Godot;
using System;
using System.Collections.Generic;
using test_platformer.Scripts.Interfaces;

public abstract partial class StateMachine<T> : Node, IStateMachine<T> where T : IState
{
	[Export]
	public NodePath InitialState;
	public IState _currentState;
	protected readonly Dictionary<string, IState> _states = new();

	public void TransitionTo(T state)
	{
		
		if (!_states.ContainsKey(state.Name) || _currentState == _states[state.Name])
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

    public void UnhandledInput(InputEvent @event)
    {
        throw new NotImplementedException();
    }

}
