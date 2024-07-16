using Godot;
using System;
using System.Collections.Generic;
using test_platformer.Scripts.Interfaces;

public abstract partial class StateMachine : Node, IStateMachine
{
	[Export]
	public NodePath InitialState;
	public IState _currentState;
	protected readonly Dictionary<string, IState> _states = new();

	public void TransitionTo(string stateName)
	{
		//GD.Print(DateTime.Now.ToString("HH:mm:ss.fff") + " - Transitioning to state: " + stateName);

		if (!_states.ContainsKey(stateName) || _currentState == _states[stateName])
		{
			return;
		}

		_currentState.Exit();
		_currentState = _states[stateName];
		//GD.Print(DateTime.Now.ToString("HH:mm:ss.fff") + " - Transitioning to state: " + _currentState.Name);
		_currentState.Enter();
	}

	public void UnhandledInput(InputEvent @event)
	{
		_currentState.HandleInput(@event);
	}

	public override void _Input(InputEvent @event)
	{
		_currentState.HandleInput(@event);
	}

}
