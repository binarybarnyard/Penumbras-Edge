using Godot;
using test_platformer.Scripts.Enums;

public partial class PlayerStateMachine : StateMachine
{
    private Player _player;
    private Vector2 _velocity = Vector2.Zero;

    // Todo: Move this to a physics settings class
    private float Gravity = GLOBALS.GRAVITY;

    public Vector2 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    public Player Player { get => _player; set => _player = value; }

    public PlayerStateMachine(string initialStateName)
    {

    }

    public override void _Ready()
    {
        GD.Print("PlayerStateMachine constructor called.");
        PopulateStates();
        Player = (Player)GetNode("/root/Player");
        _currentState = _states[_player.InitialStateName];
        _currentState.Enter();
    }

    private void PopulateStates()
    {
        var children = GetChildren();
        foreach (var child in children)
        {
            if (child is PlayerState state)
            {
                state.StateMachine = this;
                _states.Add(state.Name, state);
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        _currentState.HandleInput(@event);
    }

    //_Process is used for non-physics updates such as animations
    public override void _Process(double delta)
    {
        _currentState.Update((float)delta);
    }

    // _PhysicsProcess is used for physics updates such as movement
    // The benefit of separating the two is that you can have a consistent frame rate for physics updates
    // While having a variable frame rate for non-physics updates
    public override void _PhysicsProcess(double delta)
    {
        GravityForce(delta);

        if (_currentState != null) _currentState.PhysicsUpdate((float)delta);
        if (_player != null) _player.MoveAndCollide(_velocity);
    }

    internal string GetCurrentStateName()
    {
        return _currentState?.Name ?? "No state";
    }

    public void GravityForce(double delta)
    {
        _velocity.Y += Gravity * (float)delta;
    }

    public void UpdateState()
    {
        // Possible States: idle, walk, jump, fall
        // Logic for each state:
        // idle: no velocity exists
        // walk: x velocity exists and no y velocity exists
        // jump: y velocity exists and is negative and the jump button is pressed and the player is on the floor
        // fall: y velocity exists and is positive

        if (_velocity == Vector2.Zero)
        {
            TransitionTo("PlayerIdle");
        }
        else if (_velocity != Vector2.Zero)
        {
            TransitionTo("PlayerMove");
        }
        else if (_velocity.Y < 0 && Input.IsActionJustPressed("jump") && _player.IsOnFloor())
        {
            TransitionTo("PlayerJump");
        }
        else if (_velocity.Y > 0)
        {
            TransitionTo("PlayerFall");
        }
    }

}

// if (Input.IsActionJustPressed("jump") && _player.IsOnFloor())
// {
//     TransitionTo("jump");
// }
// else if (_velocity != Vector2.Zero)
// {
//     TransitionTo("walk");
// }
// else
// {
//     TransitionTo("idle");
// }