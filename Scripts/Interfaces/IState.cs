using Godot;

namespace test_platformer.Scripts.Interfaces
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update(double delta);
        public void PhysicsUpdate(double delta);
        public void HandleInput(InputEvent @event);
    }
}