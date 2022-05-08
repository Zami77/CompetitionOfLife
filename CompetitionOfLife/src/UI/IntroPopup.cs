using Godot;
using System;

public class IntroPopup : AcceptDialog
{
	private bool poppedUp = false;
	public override void _Ready()
	{
	}

	public override void _Process(float delta)
	{
		if (!poppedUp)
		{
			this.PopupCentered();
			poppedUp = true;
		}
	}
}
