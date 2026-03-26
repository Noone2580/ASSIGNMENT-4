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

    BasePlayer[] Players = new BasePlayer[1];
    BaseRoom CurrentRoom = new BaseRoom();

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

        CurrentRoom.Setup(this);
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new BasePlayer();
            Players[i].Setup();
        }
    }

    public BasePlayer[] GetAllPlayers() 
    {
        return Players;
    }

    public Vector2[] GetAllPlayerPositions() 
    {
        Vector2[] PlayerPositions = new Vector2[Players.Length];
        for (int i = 0; i < Players.Length; i++)
        {
            PlayerPositions[i] = Players[i].Position;
        }
        return PlayerPositions;
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        // Reset background
        Window.ClearBackground(Color.OffWhite);

        CurrentRoom.Render();

        if (Input.IsKeyboardKeyDown(KeyboardInput.W))
            Players[0].Move(new Vector2(0, -1));
        if (Input.IsKeyboardKeyDown(KeyboardInput.S))
            Players[0].Move(new Vector2(0, 1));
        if (Input.IsKeyboardKeyDown(KeyboardInput.A))
            Players[0].Move(new Vector2(-1, 0));
        if (Input.IsKeyboardKeyDown(KeyboardInput.D))
            Players[0].Move(new Vector2(1, 0));

        Players[0].Render();
    }

}