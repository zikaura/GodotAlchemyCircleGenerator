using Godot;
using System;

public class GradientAnimator : Panel
{
    private Gradient gradient;
    private GradientTexture gradientTexture;
    private StyleBoxTexture gradientStyleBox;

    // Animation Properties
    private Vector2 clockHandA;
    private Vector2 clockHandB;
    
    private Color colorA;
    private Color colorB;
    private float timeScale = 0.60f;
    private float stepValue = 0.25f;
    private OpenSimplexNoise noise;

    public override void _Ready()
    {
        gradientStyleBox = (StyleBoxTexture) Get("custom_styles/panel");
        gradientTexture  = (GradientTexture) gradientStyleBox.Texture;
        gradient = gradientTexture.Gradient;

        colorA = gradient.Colors[0];
        colorB = gradient.Colors[1];
        
        clockHandA = Vector2.Up;
        clockHandB = Vector2.Down;
        // "Clock Hands" will remain orthogonal to each other
        
        noise = new OpenSimplexNoise();
        noise.Seed = (int) GD.Randi();
        noise.Octaves = 3;
        noise.Period = 20.0f; 
        noise.Persistence = 0.8f;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (gradient != null) AnimateGradient(delta);
    }

    /// <summary>
    ///     Custom gradient animation algorithm.
    ///     It works best with perlin noise but alas...
    ///</summary>
    public void AnimateGradient(float delta)
    {
        clockHandA = clockHandA.Rotated(delta * timeScale);
        clockHandB = clockHandB.Rotated(delta * timeScale);
        var colorAFactor = Wrapf(noise.GetNoise2dv(clockHandA), 0.1f, 1.0f);
        var colorBFactor = Wrapf(noise.GetNoise2dv(clockHandB), 0.1f, 1.0f);
        var colorAHue = colorA.h + (stepValue * timeScale * noise.GetNoise1d(delta));
        var colorBHue = colorB.h + (stepValue * timeScale * noise.GetNoise1d(delta));

        colorA = Color.FromHsv(Mathf.Lerp
            (colorA.h, colorAHue, colorAFactor),
                            colorA.s, colorA.v);

        colorB = Color.FromHsv(Mathf.Lerp
            (colorB.h, colorBHue, colorBFactor),
                            colorB.s, colorB.v);
        
        gradient.Colors = new Color[] { colorA, colorB };
    }

    /// <summary>Float alternative for Mathf.Wrap()</summary>
    public float Wrapf(float val, float min = 0.0f, float max = 1.0f)
    {
        if ((val < min) || (val > max)) return min;
        return val;
    }
}
