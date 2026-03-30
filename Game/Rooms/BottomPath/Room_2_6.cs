using MohawkGame2D;
using System.Numerics;

public class Room_2_6 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "P";
        MapPosition = new Vector2(2, 6);
        RoomCode = 0;

        Doors = new BaseDoor[1];
        ConectedRooms = new BaseRoom[1];

        AddDoor(0,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Room_2_5());
    }
}
