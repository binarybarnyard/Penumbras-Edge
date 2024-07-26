using gamejam15.Scripts.Classes.Utilities;
using gamejam15.Scripts.InterFaces;
using Godot;

namespace gamejam15.Scripts.Classes
{
    public partial class CharacterBody : CharacterBody2D, ICharacter
    {
        [Export] public float Speed = 150f;
        [Export] public int Damage = 1;
        [Export] public int HitPoints = 1;
        [Export] public float JumpVelocity = -200.0f;

        public Vector2 _velocity = Vector2.Zero;
        public float Gravity = SettingsFetcher.Gravity();

        public void GravityForce(double delta)
        {
            if (!IsOnFloor())
            {
                _velocity.Y += Gravity * (float)delta;
            }
        }

        public virtual void TakeDamage(int _receivedDamage)
        {
            HitPoints -= _receivedDamage;
            if (HitPoints <= 0)
            {
                // Implement death logic here
            }
        }
    }
}