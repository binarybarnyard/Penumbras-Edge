using gamejam15.Scripts.Classes;
using Godot;

public partial class Pops : CharacterBody
{
	// Implementing the first non-hostile npc
	// R-G Tests:
	// [Passed] - NPC Appears on screen
	// [Passed] - NPC has an idle animation
	// [Passed] - NPC Has proper collision
	// [Passed] - NPC Responds to gravity
	// [Passed] - NPC shifts idle animations randomly
	// [Req Changed] - NPC changes to an idle animation when player is near
	// [Req Changed] - NPC naturally returns to idle when the player leaves the area
	// [Passed] - NPC has a dialogue box that appears when the player is near
	// [Passed] - Player can interact with the NPC to trigger a console response
	// [Passed] - Player interaction causes a dialogue box to appear
	// [Req Changed] - Player interaction causes the NPC to change to a talking animation
	// [Failed] - NPC changes something about the player (Starting with adding a heart to the player)

	// public variables
	private AnimatedSprite2D animatedSprite2D;
	private Player player;
	private Timer tick;
	private string currentAnimation { get; set; }
	private bool animationPlayed { get; set; } = false;

	// overrides
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("Sprite");
		animatedSprite2D.AnimationLooped += () =>
		{
			animationPlayed = true;
		};
		animatedSprite2D.AnimationChanged += () =>
		{
			animationPlayed = false;
		};

		animatedSprite2D.Play("idle0");
		player = GetNode<Player>("/root/MainScene/Player");
		tick = GetNode<Timer>("Tick");
		tick.Autostart = true;
		currentAnimation = "idle1";
		GD.Print("Pops is ready!");
		tick.Connect("timeout", new Callable(this, nameof(AnimationCycle)));
	}

	private void AnimationCycle()
	{
		// Randomize the idle animation
		// let the last animation play out before changing
		if (animationPlayed)
		{
			var random = new RandomNumberGenerator();
			random.Randomize();
			var idle = random.RandiRange(0, 2);
			currentAnimation = "idle" + idle;
			animatedSprite2D.Play(currentAnimation);
		}
	}

}
