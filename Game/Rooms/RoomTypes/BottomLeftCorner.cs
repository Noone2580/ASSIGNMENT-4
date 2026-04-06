using MohawkGame2D;
using System.Numerics;

/// <summary>
///     Checks to see if there is a room to the right of it and above it.
/// </summary>
public class BottomLeftCorner : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "Start";
        Doors = new BaseDoor[2];
        LightIndex = 3;

        AddDoor(0,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new Vector2(1f, 0f));

        AddDoor(1,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Vector2(0f, -1f));

    }
}