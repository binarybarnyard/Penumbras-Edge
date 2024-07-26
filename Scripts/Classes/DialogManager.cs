using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

namespace gamejam15.Scripts.Classes
{
	public partial class DialogManager : Control
	{
		private Label currentText;
		private Button nextButton;
		private Panel dialogWindow;


		private Queue<string> _dialogQueue = new();
		public bool isDialogActive = false;

		public override void _Ready()
		{
			nextButton = GetNode<Button>("DialogWindow/NextButton");
			currentText = GetNode<Label>("DialogWindow/CurrentText");
			dialogWindow = GetNode<Panel>("DialogWindow");
			currentText.Text = "";
			nextButton.Pressed += OnNextButtonPressed;
			nextButton.Disabled = false;
			HideDialog();
		}

		public void StartDialog(string[] lines)
		{
			_dialogQueue = new Queue<string>(lines);
			isDialogActive = true;
			ShowNextLine();
			ShowDialog();
		}

		private void ShowNextLine()
		{
			if (_dialogQueue.Count > 0)
			{
				currentText.Text = _dialogQueue.Dequeue();
			}
			else
			{
				EndDialog();
			}
		}

		public void OnNextButtonPressed()
		{
			if (isDialogActive)
			{
				ShowNextLine();
			}
		}

		private void EndDialog()
		{
			isDialogActive = false;
			HideDialog();
		}

		public void ShowDialog()
		{
			// Show the dialog UI
			nextButton.Visible = true;
			dialogWindow.Visible = true;
			Visible = true;
		}

		public void HideDialog()
		{
			// Hide the dialog UI
			nextButton.Visible = false;
			dialogWindow.Visible = false;
			Visible = false;
		}
	}
}
