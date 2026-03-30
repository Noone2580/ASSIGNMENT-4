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
    public Texture2D RoomLightTexture;
    public string RoomTextureLocation = "../../../Assets/Textures/T_Floor.png";
    public string RoomLightTextureLocation = "../../../Assets/Textures/T_Floor_Lighting_A.png";

    Game? GetGame;

    public string RoomName { get; protected set; } = "";
    public int RoomCode { get; protected set; } = 0;
    public bool IsBossRoom { get; protected set; } = false;
    public Vector2 MapPosition { get; protected set; } = Vector2.Zero;


    public void Setup(Game game)
    {
        GetGame = game;

        LeftWallCal = 50;
        RightWallCal = Window.Width - 50;
        TopWallCal = 50;
        BottomWallCal = Window.Height - 50;

        CustomSetup();

        RoomTexture = Graphics.LoadTexture(RoomTextureLocation);
        RoomLightTexture = Graphics.LoadTexture(RoomLightTextureLocation);
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

    public void AddDoor(int index, Vector2 position, Vector2 endPosition, BaseRoom connectedRoom)
    {
        Doors[index] = new BaseDoor();
        Doors[index].Position = position;
        Doors[index].EndPosition = endPosition;
        Doors[index].Setup();

        ConectedRooms[index] = connectedRoom;
    }

    public void CheckIfPlayerInDoor()
    {
        if (GetGame == null)
            return;

        Vector2[] Players = GetGame.GetAllPlayerPositions();
        bool Reset = true;

        for (int i = 0; i < Doors.Length; i++)
        {
            if (GetGame.CanUseDoor)
            {
                if (Vector2.Distance(Players[0], Doors[i].Position) <= 80)
                {
                    Graphics.UnloadTexture(RoomTexture);
                    Graphics.UnloadTexture(RoomLightTexture);
                    GetGame.EnterNewRoom(ConectedRooms[i], Doors[i].EndPosition, Doors[i].Position);
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

    public void DrawMiniMap()
    {
        float boxW = 62;
        float boxH = 38;
        float startX = 120;
        float startY = 80;
        float stepX = 76;
        float stepY = 46;

        Draw.LineColor = Color.Black;
        Draw.LineSize = 4;

        for (int i = 0; i < ConectedRooms.Length; i++)
        {
            if (i >= Doors.Length)
                break;

            if (ConectedRooms[i] == null)
                continue;

            Vector2 p1 = new Vector2(startX + (MapPosition.X * stepX), startY + (MapPosition.Y * stepY));
            Vector2 p2 = new Vector2(startX + (ConectedRooms[i].MapPosition.X * stepX), startY + (ConectedRooms[i].MapPosition.Y * stepY));

            Draw.Line(p1, p2);
        }

        Draw.LineColor = Color.Red;
        Draw.LineSize = 3;
        Draw.FillColor = Color.Clear;

        Draw.Rectangle(startX + (MapPosition.X * stepX) - (boxW / 2), startY + (MapPosition.Y * stepY) - (boxH / 2), boxW, boxH);

        switch (RoomCode)
        {
            case 0:
                Draw.LineColor = Color.Blue;
                Draw.Circle(new Vector2(startX + (MapPosition.X * stepX), startY + (MapPosition.Y * stepY)), 14);
                break;

            case 1:
                Draw.LineColor = Color.Green;
                Draw.Circle(new Vector2(startX + (MapPosition.X * stepX), startY + (MapPosition.Y * stepY)), 14);
                break;

            case 2:
                Draw.LineColor = Color.Black;
                Draw.Circle(new Vector2(startX + (MapPosition.X * stepX), startY + (MapPosition.Y * stepY)), 10);
                Text.Draw("Boss", (int)(startX + (MapPosition.X * stepX) + 25), (int)(startY + (MapPosition.Y * stepY) + 10));
                break;
        }

        Text.Draw("Start", 25, (int)(startY + (4 * stepY)));
    }

    public virtual void Render()
    {
        Graphics.Draw(RoomTexture, 0, 0);

        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Render();
        }

        CheckIfPlayerInDoor();

        if (RoomName != "")
            Text.Draw(RoomName, Window.Width / 2, 40);
    }
}
