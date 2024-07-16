using Godot;

namespace test_platformer.Scripts.Interfaces
{
    public interface IStateMachine
    {
        public void TransitionTo(string stateName);
        public void UnhandledInput(InputEvent @event);
    }
}