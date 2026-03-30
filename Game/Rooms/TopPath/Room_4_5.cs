using MohawkGame2D;
using System.Numerics;

public class Room_4_5 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "K";
        MapPosition = new Vector2(4, 5);
        RoomCode = 0;

        Doors = new BaseDoor[1];
        ConectedRooms = new BaseRoom[1];

        AddDoor(0,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Room_4_4());
    }
}
