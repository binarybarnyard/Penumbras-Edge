using gamejam15.Scripts.Classes;
using Godot;

public partial class Pops : CharacterBody
{
    // Implementing the first non-hostile npc
    // R-G Tests:
    // [Passed] - NPC Appears on screen
    // [Passed] - NPC has an idle animation
    // [Failed] - NPC Has proper collision
    // [Failed] - NPC Responds to gravity
    // [Failed] - NPC shifts idle animations randomly
    // [Failed] - NPC changes to an idle animation when player is near
    // [Failed] - NPC naturally returns to idle when the player leaves the area
    // [Failed] - NPC has a dialogue box that appears when the player is near
    // [Failed] - Player can interact with the NPC to trigger a console response
    // [Failed] - Player interaction causes a dialogue box to appear
    // [Failed] - Player interaction causes the NPC to change to a talking animation
    // [Failed] - NPC changes something about the player (Starting with adding a heart to the player)

    // public nodes
    private AnimatedSprite2D animatedSprite2D;
    private Area2D triggerArea;
    private CollisionShape2D collision;
    private Player player;
    private Timer tick;
    private string currentAnimation { get; set; }
    private bool animationPlayed { get; set; } = false;
    private bool playerOverlapping { get; set; }
    private DialogManager dialogManager { get; set; }

    // public variables

    // private variables

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
        triggerArea = GetNode<Area2D>("TriggerArea");
        collision = GetNode<CollisionShape2D>("TriggerArea/Collision");
        player = GetNode<Player>("/root/MainScene/Player");
        tick = GetNode<Timer>("Tick");
        tick.Autostart = true;
        currentAnimation = "idle1";
        GD.Print("Pops is ready!");
        tick.Connect("timeout", new Callable(this, nameof(AnimationCycle)));
        triggerArea.Monitoring = true;

        dialogManager = GetNode<DialogManager>("DialogManager");
    }

    public override void _Process(double delta)
    {
        if (triggerArea.OverlapsBody(player))
        {
            animatedSprite2D.Play("idle1");
            playerOverlapping = true;

            if (Input.IsActionJustPressed("attack"))
            {
                if (dialogManager.isDialogActive)
                {
                    dialogManager.OnNextButtonPressed();
                }
                else
                {
                    dialogManager.StartDialog(new string[] { "What're ya buyin'?", "Hehehe that's a good one, stranjuh.", "I'm just a humble merchant, but I'm sure we can make a deal." });
                }
            }
        }
        else
        {
            playerOverlapping = false;
        }
    }

    private void AnimationCycle()
    {
        // Randomize the idle animation
        // let the last animation play out before changing
        if (animationPlayed && !playerOverlapping)
        {
            var random = new RandomNumberGenerator();
            random.Randomize();
            var idle = random.RandiRange(0, 2);
            currentAnimation = "idle" + idle;
            animatedSprite2D.Play(currentAnimation);
        }
    }

}
