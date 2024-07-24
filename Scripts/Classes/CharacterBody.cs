using gamejam15.Scripts.Classes.Utilities;
using gamejam15.Scripts.InterFaces;
using Godot;

namespace gamejam15.Scripts.Classes
{
    public partial class CharacterBody : CharacterBody2D, ICharacter
    {
        public Vector2 _velocity = Vector2.Zero;
        private float _gravity = SettingsFetcher.Gravity();
        public void GravityForce(double delta)
        {
            if (!IsOnFloor())
            {
                _velocity.Y += _gravity * (float)delta;
            }
        }


        public override void _PhysicsProcess(double delta)
        {
            GravityForce(delta);
            MoveAndSlide();
        }

    }
}