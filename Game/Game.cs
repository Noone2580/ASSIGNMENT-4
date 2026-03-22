// Include the namespaces (code libraries) you need below.
using Microsoft.VisualBasic;
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    // Place your variables here:
    Vector2 Start = new Vector2(Window.Width / 2, Window.Height / 2);

    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        // Set up window
        Window.SetTitle("TEST");
        Window.SetSize(800, 600);
        // Remove outlines
        Draw.LineColor = Color.Clear;
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        // Reset background
        Window.ClearBackground(Color.OffWhite);

        if (Input.IsKeyboardKeyDown(KeyboardInput.Down))
            Start += new Vector2(0, 1);

        Draw.FillColor = Color.Red;
        Draw.Circle(Start, 50);

        Vector2 Dir = Vector2.Normalize( Input.GetMousePosition() - Start);

        Draw.FillColor = Color.Blue;
        Draw.Circle(Start + (Dir * 100), 30);
    }

}