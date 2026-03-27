using System;
using System.Numerics;
using MohawkGame2D;

/// <summary>
///     This Class has the base functions and variables that all rooms inhairint form.
/// </summary>
public class BaseRoom
{
    public BaseDoor[] Doors = new BaseDoor[4];

    public BaseRoom[] ConectedRooms = new BaseRoom[4];

    public float LeftWallCal;
    public float RightWallCal;
    public float TopWallCal;
    public float BottomWallCal;

    public Texture2D RoomTexture;
    public string RoomTextureLocation = "../../../Assets/Textures/BaseRoom.png";

    Game GetGame;

    public void Setup(Game game)
    {
        GetGame = game;
        LeftWallCal = 50;
        RightWallCal = Window.Width - 50;

        TopWallCal = 50;
        BottomWallCal = Window.Height - 50;

        CustomSetup();

        RoomTexture = Graphics.LoadTexture(RoomTextureLocation);
    }


    public virtual void CustomSetup()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i] = new BaseDoor();

            switch (i)
            {
                case 0:
                    Doors[i].Position = new Vector2(0, Window.Height / 2);
                    Doors[i].EndPosition = new Vector2(Window.Width, Window.Height / 2);

                    break;
                case 1:
                    Doors[i].Position = new Vector2(Window.Width / 2, 0);
                    Doors[i].EndPosition = new Vector2(Window.Width / 2, Window.Height);

                    break;
                case 2:
                    Doors[i].Position = new Vector2(Window.Width, Window.Height / 2);
                    Doors[i].EndPosition = new Vector2(0, Window.Height / 2);

                    break;
                case 3:
                    Doors[i].Position = new Vector2(Window.Width / 2, Window.Height);
                    Doors[i].EndPosition = new Vector2(Window.Width / 2, 0);

                    break;
            }
            Doors[i].Setup();
        }

        

    }


    public void CheckIfPlayerInDoor()
    {
        Vector2[] Players = GetGame.GetAllPlayerPositions();

        bool Reset = true;

        for (int i = 0; i < Doors.Length; i++)
        {
            if (GetGame.CanUseDoor)
            {
                if (Vector2.Distance(Players[0], Doors[i].Position) <= 80)
                {
                    Graphics.UnloadTexture(RoomTexture);
                    GetGame.EnterNewRoom(ConectedRooms[i], Doors[i].EndPosition);
                    Reset = false;
                    break;
                }
            }
            else if (Vector2.Distance(Players[0], Doors[i].Position) >= 80)
            {
                GetGame.CanUseDoor = Reset;
            }
        }
    }

    public virtual void Render()
    {
        Graphics.Draw(RoomTexture, 0, 0);

        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Render();
        }

        CheckIfPlayerInDoor();

    }
}
