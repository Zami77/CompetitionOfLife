using System;
using System.Collections.Generic;

public class StartingPattern
{
	public List<(int x, int y)> Cells { get; set; }

	public StartingPattern(List<(int x, int y)> _cells)
	{
		Cells = _cells;
	}
}
