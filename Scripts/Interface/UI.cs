using Godot;
using System;

public partial class UI : Node
{
    // Nodes on the UI
    private PopupMenu optionsMenu;
    public override void _Ready()
    {
        optionsMenu = GetNode<PopupMenu>("Canvas/OptionsMenu");
        GD.Print("optionsMenu: " + optionsMenu);
        optionsMenu.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("openMenu"))
        {
            GD.Print("Bang!");
            optionsMenu.Visible = !optionsMenu.Visible;
            //Pause the game if the menu is visible
        }
    }
}
