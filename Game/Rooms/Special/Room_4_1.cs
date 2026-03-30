using MohawkGame2D;
using System.Numerics;

public class Room_4_1 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "H";
        MapPosition = new Vector2(4, 1);
        RoomCode = 0;

        Doors = new BaseDoor[2];
        ConectedRooms = new BaseRoom[2];

        AddDoor(0,
            new Vector2(0, Window.Height / 2),
            new Vector2(Window.Width - 100, Window.Height / 2),
            new Room_3_1());

        AddDoor(1,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Room_4_2());
    }
}
