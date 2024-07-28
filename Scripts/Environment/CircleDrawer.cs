using Godot;

public partial class CircleDrawer : Node2D
{
	[Export] public float Radius = 0.0f;
	[Export] public Color Color = new Color(0, 0, 0);
	[Export] public float GrowthRate = 0.025f;

	public override void _Process(double delta)
	{
		// Increase the radius
		Radius += GrowthRate;
		
		// Request a redraw
		QueueRedraw();
	}

	public override void _Draw()
	{
		// _Draw is called only once
		DrawCircle(Vector2.Zero, Radius, Color);
	}
}
