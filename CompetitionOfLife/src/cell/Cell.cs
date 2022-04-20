using Godot;
using System;

public class Cell : Node2D
{
	Vector2 Location { get; set; }
	CellState State { get; set; } = CellState.Empty;
	CellColor Color { get; set; } = CellColor.White;
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
