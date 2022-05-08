using Godot;
using System;

public class GameController : Node2D
{
	[Signal]
	public delegate void GameResults(int blueScore, int redScore);
	[Signal]
	public delegate void GameReset();
	private int redScore = 0;
	private int blueScore = 0;
	private bool redFlag = false;
	private bool blueFlag = false;
	private const string blueName = "BoardBlue";
	private const string redName = "BoardRed";

	public override void _Ready()
	{
		Board blueBoard = this.GetNode<Board>("../BoardBlue");
		Board redBoard = this.GetNode<Board>("../BoardRed");
		GameResultsDialog gameResultsDialog = this.GetNode<GameResultsDialog>("GameResultsDialog");

		blueBoard.Connect(nameof(Board.FinalScore), this, nameof(TallyScore));
		redBoard.Connect(nameof(Board.FinalScore), this, nameof(TallyScore));
		gameResultsDialog.Connect("confirmed", this, nameof(OnDialogConfirmation));
	}

	private void TallyScore(int finalScore, string boardName)
	{
		switch (boardName)
		{
			case blueName:
				blueScore = finalScore;
				blueFlag = true;
				break;
			case redName:
				redScore = finalScore;
				redFlag = true;
				break;
			default:
				break;
		}

		if (blueFlag && redFlag)
		{
			this.EmitSignal(nameof(GameResults), blueScore, redScore);
		}
	}

	private void OnDialogConfirmation()
	{
		this.EmitSignal(nameof(GameReset));
	}
}
