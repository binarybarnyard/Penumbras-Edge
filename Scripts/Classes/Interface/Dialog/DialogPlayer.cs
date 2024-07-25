using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using gamejam15.Scripts.Classes.Data;
using Godot;

public partial class DialogPlayer : CanvasLayer
{
	[Export(PropertyHint.File, "*.json")]
	public string SceneTextFile;

	private Dictionary<string, List<DialogData>> sceneText = new();
	private List<string> selectedText = new();
	private bool inProgress = false;
	private Label dialogLabel;
	private Label nameLabel;

	public override void _Ready()
	{
		dialogLabel = GetNode<Label>("Dialog/DialogLabel");
		nameLabel = GetNode<Label>("Name/NameLabel");
		HideAll();
		LoadSceneText();
		SignalBus.DisplayDialogEvent += OnDisplayDialog;
	}

	private void LoadSceneText()
	{
		var file = FileAccess.Open(SceneTextFile, FileAccess.ModeFlags.Read);
		if (file != null)
		{
			var contents = file.GetAsText();
			sceneText = JsonSerializer.Deserialize<Dictionary<string, List<DialogData>>>(contents);
			GD.Print("Loaded scene text : " + contents);
		}
		else
		{
			GD.PrintErr("Error: Unable to load scene text file.");
		}
	}

	private void OnDisplayDialog(string key)
	{
		GD.Print("Displaying dialog: " + key);
		if (inProgress)
		{
			NextLine();
		}
		else
		{
			if (sceneText[key].Any())
			{
				// Move the dialog box up the 100px
				inProgress = true;
				ShowAll();
				GetTree().Paused = true;
				// Selected text becomes a queue of strings from the sceneText dictionary
				// Make a new list so the old object isn't modified
				selectedText = new List<string>(sceneText[key].SelectMany(x => x.Dialog));
				nameLabel.Text = sceneText[key].First().Name;
				// Display the first line of text
				NextLine();
			}
		}
	}

	private void NextLine()
	{
		// Anything remains in the list of text
		if (selectedText.Any())
		{
			dialogLabel.Text = selectedText.First();
			selectedText.RemoveAt(0);
		}
		// Done talking, hide the display and resume the game
		else
		{
			Finish();
		}
	}

	private void Finish()
	{
		HideAll();
		inProgress = false;
		GetTree().Paused = false;
	}

	private void HideAll()
	{
		dialogLabel.Text = "";
		GetChildren(true).ToList().ForEach(outer =>
		{
			GD.Print("Child Node: " + outer.Name);
			outer.GetChildren().ToList().ForEach(inner =>
			{
				if (inner is Label label)
				{
					label.Visible = false;
				}
				else if (inner is TextureRect rect)
				{
					rect.Visible = false;
				}
			});
		});
	}

	private void ShowAll()
	{
		GetChildren(true).ToList().ForEach(outer =>
		{
			GD.Print("Child Node: " + outer.Name);
			outer.GetChildren().ToList().ForEach(inner =>
			{
				if (inner is Label label)
				{
					label.Visible = true;
				}
				else if (inner is TextureRect rect)
				{
					rect.Visible = true;
				}
			});
		});
	}
}
