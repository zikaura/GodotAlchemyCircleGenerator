using Godot;
using System;

[Tool]
public class ShapeGenerator : Node
{
    private bool _generate;
    [Export] public int size = 512;
    [Export] public int thickness;
    [Export] public Color color;
    [Export] public Color backgroundColor;
    [Export] public bool generate
    {
        set
        {
            _generate = value;
            RegenerateImage();
        }
        get => _generate;
    }

    // Must point to a Node with a "texture" property
    [Export] public NodePath renderTargetNP;

    public Alchemist circleGenerator;
    public ImageTexture texture;
    public Node renderTarget;

    public override void _Ready()
    {
        renderTarget = GetNode(renderTargetNP);
        circleGenerator = new Alchemist(color, backgroundColor, thickness, size);
        RegenerateImage();
    }

    public void RegenerateImage()
    {
        GD.Print("Got Here 1");
        if (renderTargetNP != null)
        {
            Image textureImage = new Image();
            circleGenerator.GenerateImage();
            textureImage.CopyFrom(circleGenerator.textureImage);
            renderTarget = GetNode(renderTargetNP);
            texture.CreateFromImage(textureImage);
            renderTarget.Set("texture", texture);
        }
        else GD.PrintErr("Render target hasn't been assigned.");
    }
}
