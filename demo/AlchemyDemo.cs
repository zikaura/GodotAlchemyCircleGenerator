using Godot;
using System;

public class AlchemyDemo : Control
{
	private SidePanel sidePanel;
	private Timer generatorTimer;
	private int currentSeedOffset;
	private int displayPanelIndex = 0;
	private Alchemist circleGenerator;
	private GridContainer gridContainer;
	private SymbolDisplay[] displayPanels;

	public override void _Ready()
    {
        gridContainer = GetNode<GridContainer>
        ("BGPanel/MainSectionContainer/MainSection/GridContainer");
        generatorTimer = GetNode<Timer>("GeneratorTimer");
		sidePanel = GetNode<SidePanel>("BGPanel/SidePanel");

        AssignSeedOffset();
        generatorTimer.Connect("timeout", this,
                nameof(_OnGeneratorTimerTimeout));

        Color backgroundColor = new Color
        (0.141176f, 0.141176f, 0.141176f);
        circleGenerator = new Alchemist
        (Colors.WhiteSmoke, backgroundColor, 4, 256);

        InitializePanelArray();
        generatorTimer.Start();
    }

	private int GetSeedOffset()
	{
		GD.Randomize();
		return Mathf.RoundToInt
		( (float) GD.RandRange(0.0, 500.0) );
	}

    private void AssignSeedOffset() => currentSeedOffset = GetSeedOffset();

    private void InitializePanelArray()
    {
        int panelCount = gridContainer.GetChildCount();
        displayPanels = new SymbolDisplay[panelCount];
        for (int i = 0; i < panelCount; i++)
        {
            displayPanels[i] = gridContainer
            .GetChild<SymbolDisplay>(i);
        }
    }

    public void _OnRegenerateButtonPressed()
	{
		if (generatorTimer.IsStopped()) generatorTimer.Start();
	}

	public ImageTexture GenerateCircle(int seedValue)
	{
		ImageTexture texture = new ImageTexture();
		circleGenerator.GenerateImage (seedValue);
		texture.CreateFromImage (circleGenerator.textureImage);
		return texture;
	}

	public void _OnGeneratorTimerTimeout()
	{
		SymbolDisplay displayPanel = displayPanels[displayPanelIndex];
		displayPanel.seedValue = displayPanelIndex + currentSeedOffset;
		displayPanel.circleTexture = GenerateCircle(displayPanelIndex + currentSeedOffset);
		displayPanelIndex++;
		
		if (displayPanelIndex == displayPanels.Length)
		{
			AssignSeedOffset();
			displayPanelIndex = 0;
			generatorTimer.Stop();
		}
	}
}
