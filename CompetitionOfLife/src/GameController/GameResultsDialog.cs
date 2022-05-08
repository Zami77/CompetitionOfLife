using Godot;
using System;

public class GameResultsDialog : AcceptDialog
{

	public override void _Ready()
	{
		GameController gameController = this.GetParent<GameController>();

		gameController.Connect(nameof(GameController.GameResults), this, nameof(DisplayResults));
	}

	private void DisplayResults(int blueScore, int redScore)
	{
		this.DialogText = 
			$"Blue: {blueScore}\nRed:  {redScore}\n{(blueScore >= redScore ? "Blue" : "Red")} wins!\nPress OK to start a new game.";
		
		this.PopupCentered();
	}
}
