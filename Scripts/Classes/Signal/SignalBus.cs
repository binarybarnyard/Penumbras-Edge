using Godot;
using System;

// Singleton class to handle signals
// Auto-loaded through Project Settings
public partial class SignalBus : Node
{
    public static SignalBus Instance { get; private set; }
    
    [Signal]
    public delegate void DisplayDialogEventHandler(string key);
    public static event DisplayDialogEventHandler DisplayDialogEvent;

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GD.PrintErr("Error: SignalBus instance already exists.");
        }
    }

    public static void EmitDisplayDialogEvent(string key)
    {
        DisplayDialogEvent?.Invoke(key);
    }
}
