using Godot;

namespace test_platformer.Scripts.Interfaces
{
    public interface IStateMachine<T> where T : IState
    {
        public void TransitionTo(T state);
        public void UnhandledInput(InputEvent @event);
    }
}