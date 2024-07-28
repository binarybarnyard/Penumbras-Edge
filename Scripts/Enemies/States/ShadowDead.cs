using Godot;

public partial class ShadowDead : State
{
	// Nodes
	protected Shadow _shadow { get; private set; }
	protected AnimatedSprite2D _animatedSprite { get; private set; }
	protected Area2D _damageZone { get; private set; }
	protected GpuParticles2D _explosion { get; private set; }
	protected Timer _timer { get; private set; }
	protected PointLight2D _shadowRadius { get; private set; }

	// Variables
	int i = 0;

	public override void _Ready()
	{
		// Nodes
		_shadow = GetParent().GetParent<Shadow>();
		_explosion = _shadow.GetNode<GpuParticles2D>("Explosion");
		_animatedSprite = _shadow.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_timer = _shadow.GetNode<Timer>("DeathTimer");
		_damageZone = _shadow.GetNode<Area2D>("DamageZone");
		_shadowRadius = _shadow.GetNode<PointLight2D>("ShadowRadius");

		// Signals
		_timer.Connect("timeout", new Callable(this, nameof(RemoveFromScene)));
	}
	
	public override void Enter()
	{
		// Logging
		GD.Print("Entering ShadowDead state.");
		
		// Animation
		_animatedSprite.Play("dead");
		_animatedSprite.SpriteFrames.SetAnimationLoop("dead", false);

		// Start timer
		_timer.Start();
	}
	
	public override void Exit()
	{
		GD.Print("Exiting ShadowDead state.");
	}

	public void RemoveFromScene()
	{
		

		GD.Print("I GOT PLAYED :D");
		// Configure explosion
		_explosion.OneShot = true; // Set to one-shot
		_explosion.Emitting = true; // Start emitting

		// Hide the shadow
		_animatedSprite.Visible = false;

		// Turn off damage area
		_damageZone.Monitorable = false;
		_damageZone.Monitoring = false;

		// Turn off surrounding shadows
		_shadowRadius.Visible = false;

		if (i == 1)
		{
			_shadow.QueueFree();
		}
		else
		{
			i++;
		}
	}
}
