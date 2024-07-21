using Godot;
using System;

public partial class HealthContainer : FlowContainer
{
	public Player player { get; set; }
	public PackedScene heartScene { get; set; }
	public PackedScene emptyHeartScene { get; set; }
	private int totalHearts { get; set; }
	private int currentHearts { get; set; }
	public override void _Ready()
	{
		// Use GetNode with casting to ensure we get a Player node
		var playerNode = GetNode<CharacterBody2D>("/root/MainScene/Player");
		player = playerNode as Player;

		// Check if casting was successful
		if (player == null)
		{
			GD.PrintErr("Error: Player node is not of type Player.");
			return;
		}

		GD.Print("Player: " + player);
		heartScene = GD.Load<PackedScene>("res://Scenes/UI/Hitpoints/FullHealth.tscn");
		emptyHeartScene = GD.Load<PackedScene>("res://Scenes/UI/Hitpoints/EmptyHealth.tscn");
		player.HealthChangedEvent += OnHealthChanged;
		totalHearts = player.TotalHitPoints;
		currentHearts = player.HitPoints;
		OnHealthChanged(currentHearts, totalHearts); // Initialize hearts display
	}

	private void OnHealthChanged(int currentHealth, int totalHealth)
	{
		GD.Print("OnHealthChanged: " + currentHealth);
		Clear();
		for (int i = 0; i < currentHealth; i++)
		{
			var heartInstance = heartScene.Instantiate<Sprite2D>();
			heartInstance.Scale = new Vector2(0.15f, 0.15f);
			var heartWidth = heartInstance.RegionRect.Size.X * 0.15f + 5;
			var heartHeight = heartInstance.RegionRect.Size.Y * 0.15f + 5;
			heartInstance.Position = new Vector2(heartWidth * (i % 10), heartHeight * (i / 10));
			AddChild(heartInstance);
		}

		for (int i = currentHealth; i < totalHealth; i++)
		{
			var emptyHeart = emptyHeartScene.Instantiate<Sprite2D>();
			emptyHeart.Scale = new Vector2(0.15f, 0.15f);
			var heartWidth = emptyHeart.RegionRect.Size.X * 0.15f + 5;
			var heartHeight = emptyHeart.RegionRect.Size.Y * 0.15f + 5;
			emptyHeart.Position = new Vector2(heartWidth * (i % 10), heartHeight * (i / 10));
			AddChild(emptyHeart);
		}
	}

	private void Clear()
	{
		foreach (Node child in GetChildren())
		{
			child.QueueFree();
		}
	}
}
