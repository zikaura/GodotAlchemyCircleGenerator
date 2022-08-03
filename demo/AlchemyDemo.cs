using Godot;
using System;

public class AlchemyDemo : Control
{
    private Alchemist circleGenerator;
    private GridContainer gridContainer;

    public override void _Ready()
    {
        gridContainer = GetNode<GridContainer>
        ("BGPanel/MainSectionContainer/MainSection/GridContainer");
        circleGenerator = new Alchemist
            ( Colors.WhiteSmoke, new Color
                (0.141176f, 0.141176f, 0.141176f),
            4, 256);
        GenerateAlchemyCircles();
    }

    public void _OnRegenerateButtonPressed()
    {
        int offset = Mathf.RoundToInt(
        (float) GD.RandRange(0.0, 1000.0));
        GenerateAlchemyCircles(offset);
    }

    public ImageTexture GenerateCircle(int seedValue)
    {
        Image textureImage = new Image ();
        ImageTexture texture = new ImageTexture();
        circleGenerator.GenerateImage (seedValue);
        textureImage.CopyFrom (circleGenerator.textureImage);
        texture.CreateFromImage (textureImage);
        return texture;
    }

    public void GenerateAlchemyCircles(int offset = 0)
    {
        var displayPanels = gridContainer.GetChildren();
        for (int i = 0; i < displayPanels.Count; i++)
        {
            SymbolDisplay displayPanel = (SymbolDisplay) displayPanels[i];
            displayPanel.seedValue = i + offset;
            displayPanel.circleTexture = GenerateCircle(i + offset);
        }
    }
}
