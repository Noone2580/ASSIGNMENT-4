using MohawkGame2D;
using System.Numerics;

/// <summary>
///     Checks to see if there is a room to the right of it and balow it.
/// </summary>
public class TopLeftCorner : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "Start";
        Doors = new BaseDoor[2];
        LightIndex = 1;

        AddDoor(0,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new Vector2(1f, 0f));

        AddDoor(1,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Vector2(0f, 1f));

    }
}