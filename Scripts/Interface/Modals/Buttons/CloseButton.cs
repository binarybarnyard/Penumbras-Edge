using Godot;
using System;

public partial class CloseButton : Button
{
	// Parent Node
	private Node _parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_parent = GetParent();
	}

    public override void _Pressed()
    {
		_parent.QueueFree();
    }

}
