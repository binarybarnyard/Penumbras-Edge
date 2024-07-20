using Godot;
using System;

public partial class OptionsMenu : PopupMenu
{
	private bool show { get; set; } = false;
	public override void _Ready()
	{
		Visible = false; // Hide the menu by default
		AddItem("Exit Game", 0);  // Add an item to the menu with Id 0
		AddItem("Close Menu", 1);  // Add an item to the menu with Id 1
		Connect("id_pressed", new Callable(this, nameof(OnIdPressed)));
	}

	private void OnIdPressed(int id)
	{
		switch (id)
		{
			case 0:
				OnExitGamePressed();
				break;
			// Handle other cases as needed
			case 1:
				ToggleVisibility();
				break;
		}
	}

	private void OnExitGamePressed()
	{
		GD.Print("Exit Game");
		GetTree().Quit();
	}

	public void ToggleVisibility()
	{
		show = !show;
		Visible = show;
		GetTree().Paused = show;
	}

}