using gamejam15.Scripts.Classes;
using Godot;

public partial class Shadow : CharacterBody
{
	// Stats
	[Export] public float Speed = 200f;

	// Nodes
	protected Shadow _shadow { get; private set; }
	protected AnimatedSprite2D AnimatedSprite { get; private set; }
	protected Timer _timer { get; private set; }
	public StateMachine fsm;

	public override void _Ready()
	{
		base._Ready();
		fsm = GetNode<StateMachine>("StateMachine");
	}
}
