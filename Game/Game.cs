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
    BaseRoom CurrentRoom = new StartingRoom();

    public bool CanUseDoor = true;

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

        Start = new Vector2(Window.Width / 2, Window.Height / 2);

        CurrentRoom.Setup(this);
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new BasePlayer();
            Players[i].Setup(this);
            Players[i].Position = Start;
        }
    }

    public float[] GetRoomCal()
    {
        float[] RoomCal = new float[4];
        for (int i = 0; i < RoomCal.Length; i++)
        {
            switch (i)
            {
                case 0:
                    RoomCal[i] = CurrentRoom.LeftWallCal; 
                    break;
                case 1:
                    RoomCal[i] = CurrentRoom.RightWallCal; 
                    break;
                case 2:
                    RoomCal[i] = CurrentRoom.TopWallCal; 
                    break;
                case 3:
                    RoomCal[i] = CurrentRoom.BottomWallCal; 
                    break;
            }
        }

        return RoomCal;
    }

    public void EnterNewRoom(BaseRoom NewRoom, Vector2 DoorPosition)
    {
        if (NewRoom != null)
        {
            CanUseDoor = false;
            CurrentRoom = NewRoom;
            CurrentRoom.Setup(this);

            for (int i = 0; i < Players.Length; i++)
            {
                Players[i].Velocity = Vector2.Zero;
                Players[i].Position = DoorPosition;
            }

        }
        else
            return;
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

        //Graphics.Tint = new Color(255/2);
        Graphics.Rotation = 0;

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