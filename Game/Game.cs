// Include the namespaces (code libraries) you need below.
using Microsoft.VisualBasic;
using System;
using System.Numerics;
using System.Threading;

// The namespace your code is in.
namespace MohawkGame2D;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    // Place your variables here:
    Vector2 Start = new Vector2(Window.Width / 2, Window.Height / 2);

    BaseCharacter Dude = new BaseCharacter();

    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        // Set up window
        Window.SetTitle("TEST");
        Window.SetSize(1100, 900);
        // Remove outlines
        Draw.LineColor = Color.Clear;

        Dude.Setup();
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        // Reset background
        Window.ClearBackground(Color.OffWhite);

        if (Input.IsKeyboardKeyDown(KeyboardInput.W))
            Dude.Move( new Vector2(0,-1) );
        if (Input.IsKeyboardKeyDown(KeyboardInput.S))
            Dude.Move( new Vector2(0,1) );
        if (Input.IsKeyboardKeyDown(KeyboardInput.A))
            Dude.Move( new Vector2(-1,0) );
        if (Input.IsKeyboardKeyDown(KeyboardInput.D))
            Dude.Move( new Vector2(1,0) );

        Dude.Render();
    }

}