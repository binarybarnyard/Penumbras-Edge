using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	[Export]
	public NodePath InitialState;
	public State _currentState;
	private Dictionary<string, State> _states;

	public override void _Ready()
	{
		_states = new Dictionary<string, State>();
		foreach (Node node in GetChildren())
		{
			if (node is State state)
			{
				GD.Print(node.Name);
				_states[node.Name] = state;
				state.fsm = this;
			}
		}

		_currentState = GetNode<State>(InitialState);
		_currentState.Enter();
	}

	public override void _Process(double delta)
	{
		_currentState.Update((float) delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		// GD.Print("State: " + _currentState.Name + " Physics Process Started");
		_currentState.PhysicsUpdate((float) delta);
	}

	public void UnhandledInput(InputEvent @event)
	{
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
}
