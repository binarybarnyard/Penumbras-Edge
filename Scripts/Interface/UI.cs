using Godot;
using System;

public partial class UI : Node
{
    // Nodes on the UI
    private OptionsMenu optionsMenu;
    private HealthContainer healthContainer;
    private CharacterScreen characterScreen;

    public override void _Ready()
    {
        optionsMenu = GetNode<OptionsMenu>("Canvas/OptionsMenu");
        healthContainer = GetNode<HealthContainer>("Canvas/HealthContainer");
        characterScreen = GetNode<CharacterScreen>("Canvas/CharacterScreen");
        GD.Print("optionsMenu: " + optionsMenu);
        SetProcessInput(true);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("openMenu"))
        {
            optionsMenu.ToggleVisibility();
        }

        if (Input.IsActionJustPressed("openCharacterScreen"))
        {
            characterScreen.ToggleVisibility();
        }
    }
}