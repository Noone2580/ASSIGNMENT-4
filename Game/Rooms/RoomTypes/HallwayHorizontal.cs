using MohawkGame2D;
using System.Numerics;

/// <summary>
///     AllWays Check to see if there is a room next to it in any direction.
/// </summary>
public class HallwayHorizontal : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "Start";
        Doors = new BaseDoor[2];
        LightIndex = 10;

        AddDoor(0,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new Vector2(1f, 0f));

        AddDoor(1,
            new Vector2(0, Window.Height / 2),
            new Vector2(Window.Width - 100, Window.Height / 2),
            new Vector2(-1f, 0f));
    }
}