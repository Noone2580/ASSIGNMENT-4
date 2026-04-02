using MohawkGame2D;
using System.Numerics;

/// <summary>
///     AllWays Check to see if there is a room next to it in any direction.
/// </summary>
public class HallwayVertical : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "Start";
        Doors = new BaseDoor[2];

        AddDoor(0,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Vector2(0f, -1f));

        AddDoor(1,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Vector2(0f, 1f));

    }
}