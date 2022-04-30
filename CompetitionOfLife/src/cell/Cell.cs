using Godot;
using System;

public class Cell : Node2D
{
	Vector2 Location { get; set; } = Vector2.Zero;
	CellState State { get; set; } = CellState.Empty;
	CellColor Color { get; set; } = CellColor.White;

	public Cell()
	{}
	public Cell(Vector2 _location)
	{
		this.Location = _location;
	}
}

public enum CellState 
{
	Empty,
	Active,
	Boosted
}

public enum CellColor
{
	White,
	Blue,
	Red
}
