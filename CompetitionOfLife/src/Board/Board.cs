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
	private int offset = 64;
	public Cell[,] Grid { get; set; }

	public Board()
	{
		Grid = new Cell[width, height];
		InitPopulateGrid();
	}

	private void InitPopulateGrid()
	{
		for(int row = 0; row < Grid.GetLength(0); row++) {
			for (int col = 0; col < Grid.GetLength(1); col++)
			{
				var newCell = new Cell(GridToPixel(row, col));
				Grid[row,col] = newCell;
				AddChild(newCell);
				
			}
		}
	}

	private Vector2 GridToPixel(int row, int column)
	{
		int new_x = x_Start + offset * column;
		int new_y = y_Start + -offset * column;

		return new Vector2(new_x, new_y);
	}
}
