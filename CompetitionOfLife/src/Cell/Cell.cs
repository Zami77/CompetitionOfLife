using Godot;
using System;

public class Cell : Node2D
{
	public Vector2 Location { get; set; } = Vector2.Zero;
	public CellState State { get; set; } = CellState.Empty;
	public CellColor Color { get; set; } = CellColor.White;
	Sprite sprite;
	Texture emptyCell;
	Texture redCell;
	Texture blueCell;
	bool initRan = false;

	public override void _Ready()
	{
		emptyCell = (Texture)ResourceLoader.Load<Texture>("res://assets/Cell/Empty_Cell.png");
		redCell = (Texture)ResourceLoader.Load<Texture>("res://assets/Cell/Red_Cell.png");
		blueCell = (Texture)ResourceLoader.Load<Texture>("res://assets/Cell/Blue_Cell.png");
		sprite = GetNode<Sprite>("Sprite");
	}
	public void Init(Vector2 _location)
	{
		Location = _location;
		Position = Location;
		initRan = true;
	}

	public void UpdateCell(CellColor newColor, CellState newState)
	{
		Color = newColor;
		State = newState;

		UpdateCellColor();
	}

	public void UpdateCell(CellState newState)
	{
		State = newState;
		Color = newState == CellState.Active ? CellColor.Blue : CellColor.White;
		UpdateCellColor();
	}

	private void UpdateCellColor()
	{
		// This is to prevent issues from FindOptimalMove,
		// as cells are not created as nodes for solving.
		if (!initRan)
		{
			return;
		}

		switch(Color) 
		{
			case CellColor.Blue:
				sprite.Texture = blueCell;
				break;
			case CellColor.Red:
				sprite.Texture = redCell;
				break;
			case CellColor.White:
			default:
				sprite.Texture = emptyCell;
				break;
		}
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
