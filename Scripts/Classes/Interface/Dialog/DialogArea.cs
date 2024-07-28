using Godot;
using System;

public partial class DialogArea : Area2D
{
	[Export]
	public string DialogKey { get; set; }
	private bool areaActive = false;

	public override void _Input(InputEvent @event)
	{
		if (areaActive && @event.IsActionPressed("attack"))
		{
			if (SignalBus.Instance == null)
			{
				GD.PrintErr("Error: SignalBus instance is null.");
				return;
			}
			SignalBus.EmitDisplayDialogEvent(DialogKey);
		}
	}

	public void _on_DialogArea_area_entered(Node body)
	{
		areaActive = true;
		GD.Print("Area entered, areaActive: " + areaActive);
	}

	public void _on_DialogArea_area_exited(Node body)
	{
		areaActive = false;
		GD.Print(DialogKey + ": Area exited, areaActive: " + areaActive);
	}

}
