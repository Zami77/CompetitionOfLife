using Godot;
using System;

public class Board : Node2D
{
	[Export]
	private int width = 8;
	[Export]
	private int height = 10;
	[Export]
	private int x_Start = 64;
	[Export]
	private int y_Start = 800;
	[Export]
	private int tileSize = 64;

	private PackedScene cell;

	private bool isPlayerTurn = true;
	private int currentRound = 1;
	private int totalRounds = 10;
	public Cell[,] Grid { get; set; }

	public Board()
	{
		Grid = new Cell[width, height];
	}

	public override void _Ready()
	{
		cell = (PackedScene)ResourceLoader.Load("res://src/Cell/Cell.tscn");
		InitPopulateGrid();
	}

	public override void _Process(float delta)
	{
		TouchInput();
	}

	private void TouchInput()
	{
		if (Input.IsActionJustPressed("ui_touch") && isPlayerTurn)
		{
			// TODO: change cell status
			var touchPos = GetGlobalMousePosition();
			var gridPos = PixelToGrid(touchPos.x, touchPos.y);
			if (IsValidGrid(gridPos))
			{
				GD.Print($"Grid Position: {gridPos.x}, {gridPos.y}");
				Grid[(int)gridPos.x, (int)gridPos.y].UpdateCell(CellColor.Blue, CellState.Active);
			}
		}
	}

	private bool IsValidGrid(Vector2 gridPos)
	{
		return gridPos.x >= 0 && gridPos.x < width &&
			gridPos.y >= 0 && gridPos.y < height;
	}

	private void InitPopulateGrid()
	{
		for(int row = 0; row < Grid.GetLength(0); row++) {
			for (int col = 0; col < Grid.GetLength(1); col++)
			{
				Cell newCell = cell.Instance<Cell>();
				newCell.Init(GridToPixel(row, col));
				AddChild(newCell);
				Grid[row,col] = newCell;
			}
		}
	}

	private Vector2 GridToPixel(int row, int column)
	{
		int new_x = x_Start + tileSize * row;
		int new_y = y_Start + -tileSize * column;

		return new Vector2(new_x, new_y);
	}

	private Vector2 PixelToGrid(float x, float y)
	{
		var new_x = (x - x_Start) / tileSize;
		var new_y = (y - y_Start) / -tileSize;
		
		return new Vector2((int)Math.Round(new_x), (int)Math.Round(new_y));
	}
}
