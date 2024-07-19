using Godot;
using System;

public partial class Slime : Area2D
{
	private int damage = 1;
	
	private void OnBodyEntered(Node body)
	{
		GD.Print("Slime! Damage: " + damage);
		if (body is Player Player)
		{
			Player.TakeDamage(damage);
		}
	}

}



