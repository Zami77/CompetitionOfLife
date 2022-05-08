public static class BoardSolver
{
    public static (int x, int y) FindOptimalMove(Cell[,] grid, CellState[,] bufferGrid)
    {
        var gridCopy = CellGridDeepCopy(grid);
        return FindOptimalMove(gridCopy, bufferGrid, (x: -1, y: -1), -1);
    }

    private static (int x, int y) FindOptimalMove(Cell[,] grid, CellState[,] bufferGrid, (int x, int y) resCoord, int resMax, int depth = 5)
    {
        // TODO: Solve for each potential cell, up to a depth of 5
        // Check base grid first, with no cell selected, then iterate
        // through all grid cells, ignoring ones which are already active

        return resCoord;
    }

    public static Cell[,] CellGridDeepCopy(Cell[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        var gridCopy = new Cell[width, height];

        for (int row = 0; row < width; row++) 
        {
            for (int col = 0; col < height; col++)
            {
                gridCopy[row, col] = new Cell();
                gridCopy[row, col].State = grid[row, col].State;
                gridCopy[row, col].Color = grid[row, col].Color;
            }
        }

        return gridCopy;
    }

    public static Cell[,] SolveGrid(Cell[,] grid, CellState[,] bufferGrid, CellColor cellColor = CellColor.Blue)
    {
        // Fill buffer grid
		for (int row = 0; row < grid.GetLength(0); row++)
		{
			for (int col = 0; col < grid.GetLength(1); col++)
			{
				bufferGrid[row, col] = BoardSolver.SolveCell(row, col, grid);
			}
		}

		// Transfer buffer grid to actual grid
		for (int row = 0; row < grid.GetLength(0); row++)
		{
			for (int col = 0; col < grid.GetLength(1); col++)
			{
				var bufferState = bufferGrid[row, col];

				grid[row, col].UpdateCell(
					bufferState == CellState.Active ? cellColor : CellColor.White, 
					bufferState
				);
			}
		}

        return grid;
    }

    private static int CountCells(Cell[,] grid)
    {
        int cells = 0;

        for (int row = 0; row < grid.GetLength(0); row++)
		{
			for (int col = 0; col < grid.GetLength(1); col++)
			{
				if (grid[row, col].State == CellState.Active)
                {
                    cells++;
                }
			}
		}

        return cells;
    }

    private static CellState SolveCell(int row, int col, Cell[,] grid)
	{
		var curNeighbors = CountNeighbors(row, col, grid);
		var curCell = grid[row, col];

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

	private static int CountNeighbors(int row, int col, Cell[,] grid)
	{
		int neighbors = 0;
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

		// Check all 8 neighbors
		if (row > 0 && grid[row - 1, col].State == CellState.Active) 
		{
			neighbors++;
		}
		
		if (row > 0 && col > 0 && grid[row - 1, col - 1].State == CellState.Active) 
		{
			neighbors++;
		}

		if (col > 0 && grid[row, col - 1].State == CellState.Active) 
		{
			neighbors++;
		}

		if (row < width - 1 && col > 0 && grid[row + 1, col - 1].State == CellState.Active) 
		{
			neighbors++;
		}

		if (row < width - 1 && grid[row + 1, col].State == CellState.Active)
		{
			neighbors++;
		}

		if (row < width - 1 && col < height - 1 && grid[row + 1, col + 1].State == CellState.Active)
		{
			neighbors++;
		}

		if (col < height - 1 && grid[row, col + 1].State == CellState.Active)
		{
			neighbors++;
		}

		if (row > 0 && col < height - 1 && grid[row - 1, col + 1].State == CellState.Active)
		{
			neighbors++;
		}

		return neighbors;
	}
}