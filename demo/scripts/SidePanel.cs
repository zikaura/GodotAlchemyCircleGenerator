using Godot;
using System;

public class SidePanel : Panel
{
    private Label seedLabel;
    private Button generateButton;
    private Button downloadButton;
    private TextureRect symbolPreview;
    private Alchemist circleGenerator;
    private AlchemyDemo mainWindow;

    public override void _Ready()
    {
        seedLabel = GetNode<Label>
        ("MarginContainer/VBoxContainer/SeedLabel");
        generateButton = GetNode<Button>
        ("MarginContainer/VBoxContainer/RGButtonContainer/Button");
        downloadButton = GetNode<Button>
        ("MarginContainer/VBoxContainer/DLButtonContainer/Button");
        symbolPreview = GetNode<TextureRect>
        ("MarginContainer/VBoxContainer/PreviewContainer/TextureRect");
        mainWindow = GetNode<AlchemyDemo>("../..");
        circleGenerator = new Alchemist
            ( Colors.WhiteSmoke, new Color
                (0.141176f, 0.141176f, 0.141176f),
            4, 256);
        _OnSymbolSelected(0, null);
    }

    public void _OnSymbolSelected(int seedValue, Texture texture)
    {
        int currentSeedValue = int.Parse
        (seedLabel.Text.Replace("#", ""));

        if ((seedValue != currentSeedValue) && (texture != null)) 
        {
            symbolPreview.Texture = texture;
            seedLabel.Text = $"#{seedValue}";
        }
    }

    public void _OnDownloadButtonPressed()
    {
        GD.Print("Download Begins");
    }

    public void _OnRegenerateButtonPressed()
    {
        // symbolPreview.Texture.Dispose();
        
        symbolPreview.Texture = null;
        mainWindow._OnRegenerateButtonPressed();
    }
}
