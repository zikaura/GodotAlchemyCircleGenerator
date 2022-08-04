using Godot;
using System;

public class AlchemyDemo : Control
{
	private int currentSeedOffset;
	private int displayPanelIndex = 0;

	private Timer generatorTimer;
	private GridContainer gridContainer;

	private Label headerLabel;
	private SidePanel sidePanel;
	private Alchemist circleGenerator;
	private SymbolDisplay[] displayPanels;

	public override void _Ready()
    {
        gridContainer = GetNode<GridContainer>
        ("BGPanel/MainSectionContainer/MainSection/GridContainer");
		headerLabel = GetNode<Label>
		("BGPanel/MainSectionContainer/MainSection/HeaderPanel/Label");
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
		((float) GD.RandRange(0.0, 500.0));
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

	public ImageTexture GenerateCircle(int seedValue)
	{
		ImageTexture texture = new ImageTexture();
		circleGenerator.GenerateImage (seedValue);
		texture.CreateFromImage (circleGenerator.textureImage);
		return texture;
	}

    private void UpdateLoadingInfo(int currentIndex)
    {
		int panelCount = gridContainer.GetChildCount();
		if (currentIndex != panelCount)
		{
			headerLabel.Text = "Symbols Generated " + 
			$"({currentIndex} / {panelCount})";
		}
		else headerLabel.Text = "Complete :)";
    }

    public void _OnRegenerateButtonPressed()
	{
		if (generatorTimer.IsStopped()) generatorTimer.Start();
	}

	public void _OnGeneratorTimerTimeout()
	{
		SymbolDisplay displayPanel = displayPanels[displayPanelIndex];
		displayPanel.seedValue = displayPanelIndex + currentSeedOffset;
		displayPanel.circleTexture = GenerateCircle(displayPanelIndex + currentSeedOffset);
		UpdateLoadingInfo(++displayPanelIndex);
		
		// All panels have been assigned to, clean up
		if (displayPanelIndex == displayPanels.Length)
		{
			AssignSeedOffset();
			displayPanelIndex = 0;
			generatorTimer.Stop();
		}
	}
}
