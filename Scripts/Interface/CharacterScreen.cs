using Godot;
using System;

public partial class CharacterScreen : PopupMenu
{
	private bool show { get; set; } = false;
	private string characterName { get; set; } = "Penumbra";
	private Player player { get; set; }
	private Label totalHealthLabel { get; set; }
	private string totalHealthLabelText
	{
		get
		{
			if (player == null)
			{
				return "Error: Player Not Detected";
			}
			return "Total Health: " + player.TotalHitPoints.ToString();
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false; // Hide the menu by default
		totalHealthLabel = GetNode<Label>("HpLabel");
		AddItem("Close Menu", 1);  // Add an item to the menu with Id 1
		Connect("id_pressed", new Callable(this, nameof(OnIdPressed)));
		player = GetNode<Player>("/root/MainScene/Player");
		totalHealthLabel.Text = totalHealthLabelText;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ToggleVisibility()
	{
		show = !show;
		Visible = show;
		GetTree().Paused = show;
	}

	private void OnIdPressed(int id)
	{
		ToggleVisibility();
	}
}
