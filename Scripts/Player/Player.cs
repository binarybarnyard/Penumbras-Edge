using gamejam15.Scripts.Classes;
using Godot;

public partial class Player : CharacterBody
{
	// Stats
	[Export]
	public int HitPoints = 5;
	[Export]
	public int TotalHitPoints { get; internal set; } = 5;
	[Export]
	public int AttackDamage = 1;

	[Export]
	public float Speed = 185.0f;
	public float JumpVelocity = -400.0f;

	[Signal]
	public delegate void HealthChangedEventHandler(int currentHealth, int totalHealth);
	public event HealthChangedEventHandler HealthChangedEvent;

	// Objects
	public StateMachine fsm;
	public Timer IFrameTimer { get; private set; }

	public override void _Ready()
	{
		fsm = GetNode<StateMachine>("StateMachine");
		IFrameTimer = GetNode<Timer>("iFrame");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}

	public void TakeDamage(int damage)
	{
		HitPoints -= damage;
		HealthChangedEvent?.Invoke(HitPoints, TotalHitPoints);
		fsm.TransitionTo("PlayerHit");
	}

	public void OnHealthChanged(int currentHealth, int totalHealth)
	{
		EmitSignal(SignalName.HealthChanged, currentHealth, totalHealth);
	}

	private void OnIFrameTimeout()
	{
		if (HitPoints > 0)
		{
			GD.Print("Hitbox enabled.");
			CollisionLayer = 1;
		}

		if (!IsOnFloor())
		{
			fsm.TransitionTo("PlayerFall");
		}
		else if (HitPoints > 0)
		{
			fsm.TransitionTo("PlayerIdle");
		}
	}
}
