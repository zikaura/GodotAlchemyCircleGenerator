using Godot;
using System;

public class SidePanel : Panel
{
    private Label seedLabel;
    private Button generateButton;
    private Alchemist circleGenerator;
    private TextureRect symbolPreview;
    private AlchemyDemo mainWindow;

    public override void _Ready()
    {
        seedLabel = GetNode<Label>
        ("MarginContainer/VBoxContainer/SeedLabel");
        generateButton = GetNode<Button>
        ("MarginContainer/VBoxContainer/ButtonContainer/Button");
        symbolPreview = GetNode<TextureRect>
        ("MarginContainer/VBoxContainer/PreviewContainer/TextureRect");
        mainWindow = GetNode<AlchemyDemo>("../..");
        circleGenerator = new Alchemist
            ( Colors.WhiteSmoke, new Color
                (0.141176f, 0.141176f, 0.141176f),
            4, 256);
        _OnSymbolSelected(0);
    }

    // Should the image be regenerated or just passed directly? 
    public void GenerateCircle(int seedValue)
    {
        Image textureImage = new Image ();
        ImageTexture texture = new ImageTexture();
        circleGenerator.GenerateImage (seedValue);
        textureImage.CopyFrom (circleGenerator.textureImage);
        texture.CreateFromImage (textureImage);
        symbolPreview.Texture = texture;
    }

    public void _OnSymbolSelected(int seedValue)
    {
        int currentSeedValue = int.Parse
        (seedLabel.Text.Replace("#", ""));

        if (seedValue != currentSeedValue) 
        {
            GenerateCircle(seedValue);
            seedLabel.Text = $"#{seedValue}";
        }
    }

    public void _OnRegenerateButtonPressed() => mainWindow._OnRegenerateButtonPressed();
}
