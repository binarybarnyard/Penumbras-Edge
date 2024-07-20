using Godot;
using System;

public partial class UI : Node
{
    // Nodes on the UI
    private OptionsMenu optionsMenu;

    public override void _Ready()
    {
        optionsMenu = GetNode<OptionsMenu>("Canvas/OptionsMenu");
        GD.Print("optionsMenu: " + optionsMenu);
        SetProcessInput(true);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("openMenu"))
        {
            optionsMenu.ToggleVisibility();
        }
    }
}