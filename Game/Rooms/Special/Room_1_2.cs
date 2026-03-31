using MohawkGame2D;
using System.Numerics;

public class Room_1_2 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "E";
        MapPosition = new Vector2(1, 2);
        RoomCode = 0;

        Doors = new BaseDoor[1];
        ConectedRooms = new BaseRoom[1];

        AddDoor(0,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Room_1_3());
    }
}
