using Godot;

namespace test_platformer.Scripts.Interfaces
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update(double delta);
        void PhysicsUpdate(double delta);
        void HandleInput(InputEvent @event);

        string Name { get; }
        string AnimationName { get; }
        IStateMachine StateMachine { get; set; }

    }
}