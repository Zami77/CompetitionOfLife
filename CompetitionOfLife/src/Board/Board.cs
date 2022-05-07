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
	[Export]
	private CellColor cellColor = CellColor.Blue;
	[Export]
	private bool isAI = false;
	private PackedScene cell;

	private int currentRound = 1;
	private int totalRounds = 10;
	public Cell[,] Grid { get; set; }
	private CellState[,] bufferGrid;
	private Vector2 selectedCell = -Vector2.One;

	public Board()
	{
		Grid = new Cell[width, height];
		bufferGrid = new CellState[width, height];
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
		if (!isAI && Input.IsActionJustPressed("ui_touch"))
		{
			var touchPos = GetGlobalMousePosition();
			var gridPos = PixelToGrid(touchPos.x, touchPos.y);
			if (IsValidGrid(gridPos) && (Grid[(int)gridPos.x, (int)gridPos.y].State == CellState.Empty || selectedCell == gridPos))
			{
 				GD.Print($"Grid Position: {gridPos.x}, {gridPos.y}");

				var curState = Grid[(int)gridPos.x, (int)gridPos.y].State;

				if (curState == CellState.Active)
				{
					Grid[(int)gridPos.x, (int)gridPos.y].UpdateCell(CellState.Empty);
				}
				else 
				{
					Grid[(int)gridPos.x, (int)gridPos.y].UpdateCell(cellColor, CellState.Active);
				}
				
				if (selectedCell != -Vector2.One) 
				{
					Grid[(int)selectedCell.x, (int)selectedCell.y].UpdateCell(CellState.Empty);
				}
				
				selectedCell = gridPos;
			}
		}
	}

	private void HandleTurn()
	{
		// Fill buffer grid
		for (int row = 0; row < Grid.GetLength(0); row++)
		{
			for (int col = 0; col < Grid.GetLength(1); col++)
			{
				bufferGrid[row, col] = SolveCell(row, col);
			}
		}

		// Transfer buffer grid to actual grid
		for (int row = 0; row < Grid.GetLength(0); row++)
		{
			for (int col = 0; col < Grid.GetLength(1); col++)
			{
				var bufferState = bufferGrid[row, col];

				Grid[row, col].UpdateCell(
					bufferState == CellState.Active ? cellColor : CellColor.White, 
					bufferState
				);
			}
		}

		selectedCell = -Vector2.One;
	}

	private CellState SolveCell(int row, int col)
	{
		var curNeighbors = CountNeighbors(row, col);
		var curCell = Grid[row, col];

		if (curCell.State == CellState.Active)
		{
			if (curNeighbors == 2 || curNeighbors == 3) {
				return CellState.Active;
			}
		}
		// Dead Cell
		else 
		{
			if (curNeighbors == 3)
			{
				return CellState.Active;
			}
		}

		return CellState.Empty;
	}

	private int CountNeighbors(int row, int col)
	{
		int neighbors = 0;

		// Check all 8 neighbors
		if (row > 0 && Grid[row - 1, col].State == CellState.Active) 
		{
			neighbors++;
		}
		
		if (row > 0 && col > 0 && Grid[row - 1, col - 1].State == CellState.Active) 
		{
			neighbors++;
		}

		if (col > 0 && Grid[row, col - 1].State == CellState.Active) 
		{
			neighbors++;
		}

		if (row < width - 1 && col > 0 && Grid[row + 1, col - 1].State == CellState.Active) 
		{
			neighbors++;
		}

		if (row < width - 1 && Grid[row + 1, col].State == CellState.Active)
		{
			neighbors++;
		}

		if (row < width - 1 && col < height - 1 && Grid[row + 1, col + 1].State == CellState.Active)
		{
			neighbors++;
		}

		if (col < height - 1 && Grid[row, col + 1].State == CellState.Active)
		{
			neighbors++;
		}

		if (row > 0 && col < height - 1 && Grid[row - 1, col + 1].State == CellState.Active)
		{
			neighbors++;
		}

		return neighbors;
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

		InitStartingPattern();
	}

	private void InitStartingPattern()
	{
		var startingPattern = StartingPatternHelper.GetStartingPattern();

		var midWidth = width / 2 - 2;
		var midHeight = height / 2 - 1;

		foreach(var cell in startingPattern.Cells)
		{
			Grid[cell.x + midWidth, cell.y + midHeight].UpdateCell(cellColor, CellState.Active);
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
	private void _on_Button_pressed()
	{
		HandleTurn();
	}
}
