using Godot;
using System;

public class Cell : Node2D
{
	Vector2 Location { get; set; } = Vector2.Zero;
	CellState State { get; set; } = CellState.Empty;
	CellColor Color { get; set; } = CellColor.White;
	Sprite sprite;
	Texture emptyCell;
	Texture redCell;
	Texture blueCell;

	public override void _Ready()
	{
		emptyCell = (Texture)ResourceLoader.Load<Texture>("res://assets/Cell/Empty_Cell.png");
		redCell = (Texture)ResourceLoader.Load<Texture>("res://assets/Cell/Empty_Cell.png");
		blueCell = (Texture)ResourceLoader.Load<Texture>("res://assets/Cell/Blue_Cell.png");
		sprite = GetNode<Sprite>("Sprite");
	}
	public void Init(Vector2 _location)
	{
		Location = _location;
		Position = Location;
	}

	public void UpdateCell(CellColor newColor, CellState newState)
	{
		Color = newColor;
		State = newState;

		UpdateCellColor();
	}

	private void UpdateCellColor()
	{
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

	private void UpdateCellState()
	{
		// TODO: Implement updating cell state
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
