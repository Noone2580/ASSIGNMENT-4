using System;
using System.Numerics;
using MohawkGame2D;

/// <summary>
///     This Class has the base functions and variables that all rooms inhairint form.
/// </summary>
public class BaseRoom
{
    BaseDoor[] Doors = new BaseDoor[4];

    Texture2D RoomTexture;
    string RoomTextureLocation = "../../../Assets/Textures/BaseRoom.png";

    Game GetGame;

    public virtual void Setup(Game game)
    {
        GetGame = game;
        
        RoomTexture = Graphics.LoadTexture(RoomTextureLocation);

        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i] = new BaseDoor();

            switch (i) 
            {
                case 0:
                    Doors[i].Position = new Vector2(0, Window.Height / 2);
                    Doors[i].EndPosition = new Vector2(Window.Width, Window.Height/2);

                    break;
                case 1:
                    Doors[i].Position = new Vector2(Window.Width / 2, 0);
                    Doors[i].EndPosition = new Vector2(Window.Width/2, Window.Height);

                    break;
                case 2:
                    Doors[i].Position = new Vector2(Window.Width,Window.Height / 2);
                    Doors[i].EndPosition = new Vector2(0, Window.Height / 2);

                    break;
                case 3:
                    Doors[i].Position = new Vector2(Window.Width/2, Window.Height);
                    Doors[i].EndPosition = new Vector2(Window.Width/2, 0);

                    break;
            }
            Doors[i].Setup(); 
        }
    }

    public virtual void Render()
    {
        BasePlayer[] PlayerPositions = GetGame.GetAllPlayers();

        Graphics.Draw(RoomTexture, 0, 0);

        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Render();

            for (int c = 0; c < PlayerPositions.Length; c++)
            {
                if (Vector2.Distance( Doors[i].Position, PlayerPositions[c].Position ) <= 100 ) 
                {
                    PlayerPositions[c].Position = Doors[i].EndPosition + new Vector2(120 ,0);
                }
            }
        }
    }
}
