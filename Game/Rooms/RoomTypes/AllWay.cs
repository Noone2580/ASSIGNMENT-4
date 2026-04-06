using MohawkGame2D;
using System.Numerics;

/// <summary>
///     AllWays Check to see if there is a room next to it in any direction.
/// </summary>
public class AllWay : BaseRoom
{
    bool[] DyLighting = new bool[4];

    public override void CustomSetup()
    {
        RoomName = "Start";
        Doors = new BaseDoor[4];

        AddDoor(0,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new Vector2(1f, 0f));

        AddDoor(1,
            new Vector2(0, Window.Height / 2),
            new Vector2(Window.Width - 100, Window.Height / 2),
            new Vector2(-1f, 0f));

        AddDoor(2,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Vector2(0f, -1f));

        AddDoor(3,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Vector2(0f, 1f));


        for (int i = 0; i < Doors.Length; i++)
        {
            DyLighting[i] = false;
            if (Doors[i] != null)
                DyLighting[i] = true;
        }

        if (DyLighting[0] && DyLighting[1] && DyLighting[2] && DyLighting[3])
        {
            LightIndex = 0;
            return;
        }
        if (DyLighting[0] && !DyLighting[1] && !DyLighting[2] && DyLighting[3])
        {
            LightIndex = 1;
            return;
        }
        if (!DyLighting[0] && DyLighting[1] && !DyLighting[2] && DyLighting[3])
        {
            LightIndex = 2;
            return;
        }
        if (DyLighting[0] && !DyLighting[1] && DyLighting[2] && !DyLighting[3])
        {
            LightIndex = 3;
            return;
        }
        if (!DyLighting[0] && DyLighting[1] && DyLighting[2] && !DyLighting[3])
        {
            LightIndex = 4;
            return;
        }
        if (DyLighting[0] && !DyLighting[1] && DyLighting[2] && DyLighting[3])
        {
            LightIndex = 5;
            return;

        }
        if (!DyLighting[0] && DyLighting[1] && DyLighting[2] && DyLighting[3])
        {
            LightIndex = 6;
            return;

        }
        if (DyLighting[0] && DyLighting[1] && DyLighting[2] && !DyLighting[3])
        {
            LightIndex = 7;
            return;

        }
        if (DyLighting[0] && DyLighting[1] && !DyLighting[2] && DyLighting[3])
        {
            LightIndex = 8;
            return;

        }
        if (!DyLighting[0] && !DyLighting[1] && DyLighting[2] && DyLighting[3])
        {
            LightIndex = 9;
            return;

        }
        if (DyLighting[0] && DyLighting[1] && !DyLighting[2] && !DyLighting[3])
        {
            LightIndex = 10;
            return;
        }

        if (DyLighting[0] || DyLighting[1])
        {
            LightIndex = 10;
            return;
        }
        if (DyLighting[2] || DyLighting[3])
        {
            LightIndex = 9;
            return;
        }
        
    }
}