using Godot;

namespace test_platformer.Scripts.Interfaces
{
    public interface IStateMachine
    {
        public void TransitionTo(IState state);
        public void UnhandledInput(InputEvent @event);
    }
}