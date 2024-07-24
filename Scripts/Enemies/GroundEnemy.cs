using gamejam15.Scripts.Classes;
using Godot;

public partial class GroundEnemy : CharacterBody
{
    // Stats
    public int Damage = 1;
    public int HitPoints = 2;
    public float Speed = 50.0f;
    public float JumpVelocity = -100.0f;

    // Nodes
    public StateMachine fsm;
    protected Area2D DamageZone { get; private set; }
    protected Area2D ThreatZone { get; private set; }
    protected Timer _timer { get; private set; }

    public override void _Ready()
    {
        // Nodes
        fsm = GetNode<StateMachine>("StateMachine");
        DamageZone = GetNode<Area2D>("DamageZone");
        ThreatZone = GetNode<Area2D>("ThreatZone");
        _timer = GetNode<Timer>("StateTimer");

        // Signals
        DamageZone.Connect("body_entered", new Callable(this, nameof(ApplyDamage)));
        ThreatZone.Connect("body_exited", new Callable(this, nameof(EndChase)));
        ThreatZone.Connect("body_entered", new Callable(this, nameof(StartChase)));
    }

    public virtual void TakeDamage(int _receivedDamage)
    {
        HitPoints -= _receivedDamage;
        GD.Print(HitPoints);

        fsm.TransitionTo("GroundHit");
    }

    public virtual void ApplyDamage(Node body)
    {

        if (body is Player Player)
        {
            GD.Print("Ground enemy! Ouch! Damage: " + Damage);
            Player.TakeDamage(Damage);
        }
    }

    public virtual void StartChase(Node body)
    {
        if (body is Player Player && HitPoints > 0)
        {
            _timer.Stop();
            fsm.TransitionTo("GroundChase");
        }
    }

    public virtual void EndChase(Node body)
    {
        if (body is Player Player && HitPoints > 0)
        {
            fsm.TransitionTo("GroundIdle");
        }
    }
}
