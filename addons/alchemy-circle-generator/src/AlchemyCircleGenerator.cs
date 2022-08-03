using System;
using Godot;

public class AlchemyCircleGenerator : Node
{
    [Export] public int size = 256;
    [Export] public int thickness;
    [Export] public Color color;
    [Export] public Color backgroundColor;

    // Must point to a Node with a "texture" property
    [Export] public NodePath renderTargetNP;
    /// <summary>If set to true, image will be automatically generated when scene starts</summary>
    [Export] public bool autoGenerate;

    public Alchemist circleGenerator;
    public ImageTexture texture;
    public Node renderTarget;

    public override void _Ready ()
    {
        texture = new ImageTexture ();
        circleGenerator = new Alchemist (
                            color, backgroundColor, 
                            thickness, size );

        if (autoGenerate)
        {
            if (renderTargetNP != null)
            {   
                renderTarget = GetNode (renderTargetNP);
                RegenerateImage (0);
            }
            else GD.PrintErr ("Render target hasn't been assigned.");
        }
    }

    public void RegenerateImage (int seed)
    {
        // int seed = Mathf.RoundToInt(
        // (float) GD.RandRange(0.0, 24.0));
        if (renderTargetNP != null)
        {
            if (renderTarget != null)
            {
                Image textureImage = new Image ();
                circleGenerator.GenerateImage (seed);
                textureImage.CopyFrom (circleGenerator.textureImage);
                renderTarget = GetNode (renderTargetNP);
                texture.CreateFromImage (textureImage);
                renderTarget.Set ("texture", texture);
            }
            else
            {
                renderTarget = GetNode (renderTargetNP);
                Image textureImage = new Image ();
                circleGenerator.GenerateImage (seed);
                textureImage.CopyFrom (circleGenerator.textureImage);
                renderTarget = GetNode (renderTargetNP);
                texture.CreateFromImage (textureImage);
                renderTarget.Set ("texture", texture);
            }
        }
        else GD.PrintErr ("Render target hasn't been assigned.");
    }

    public void _OnButtonPressed()
    {
        string seedInput = GetNode<TextEdit>
        ("../TextFieldContainer/TextEdit").Text;
        int seed = int.Parse(seedInput);
        RegenerateImage(seed);
    }
}