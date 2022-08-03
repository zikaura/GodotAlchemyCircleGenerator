using Godot;
using System;

public class SymbolDisplay : Panel
{
	private Label hexLabel;
	private SidePanel sidePanel;
	private TextureRect displayRect;

	public int seedValue
	{
		set => hexLabel.Text = $"#{value}";
		get => int.Parse(hexLabel.Text.Replace("#", ""));
	}
	
	public Texture circleTexture
	{
		set => displayRect.Texture = value;
		get => (Texture) displayRect.Texture;
	}

	public override void _Ready()
	{
		hexLabel    = GetNode<Label>("VBoxContainer/HexLabel");
		sidePanel   = GetNode<SidePanel>("../../../../SidePanel");
		displayRect = GetNode<TextureRect>("VBoxContainer/TextureRect");
	}

	public void _OnPanelGUIInput(InputEvent _event)
	{
		if (_event.GetType() == typeof(InputEventMouseButton))
		{
			if ( (((InputEventMouseButton) _event)).Pressed && circleTexture != null )
			{
				sidePanel._OnSymbolSelected(seedValue, circleTexture);
			}
		}
	}
}
