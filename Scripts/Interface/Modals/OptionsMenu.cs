using Godot;
using System;

public partial class OptionsMenu : PopupMenu
{
	public override void _Ready()
	{
		Visible = false; // Hide the menu by default
		Connect("id_pressed", new Callable(this, nameof(OnIdPressed)));
		AddItem("Exit Game", 0);  // Add an item to the menu with Id 0

		// Connect the about_to_show and about_to_hide signals to handle pausing
		Connect("about_to_show", new Callable(this, nameof(OnAboutToShow)));
		Connect("about_to_hide", new Callable(this, nameof(OnAboutToHide)));
		GD.Print("Options Menu Ready");
	}

	private void OnIdPressed(int id)
	{
		switch (id)
		{
			case 0:
				OnExitGamePressed();
				break;
				// Handle other cases as needed
		}
	}

	private void OnExitGamePressed()
	{
		GD.Print("Exit Game");
		GetTree().Quit();
	}

	private void OnAboutToShow()
	{
		GetTree().Paused = true; // Pause the game
	}

	private void OnAboutToHide()
	{
		GetTree().Paused = false; // Unpause the game
	}
}